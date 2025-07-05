using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    // MaooMAnagerのシングルトン
    public static MapManager Instance { get; private set; }

    // タイルマップ
    [Header("Area Tilemaps")]
    public AreaTilemap[] areatilemaps;

    // 経路探索や座標返還に使う共通マップ
    public Tilemap mainTilemap;

    // タイルを保持する辞書
    public Dictionary<Vector3Int, AreaType> areaMap = new Dictionary<Vector3Int, AreaType>();

    // 配置可能エリア
    public enum AreaType
    {
        None,
        DeployArea,
        HighGroundArea
    }

    private void Awake()
    {
        // 自分自身をInstanceに登録
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 最初の DeployArea タイルマップを代表マップとして使う
        foreach (var areaTM in areatilemaps)
        {
            if (areaTM.areaType == AreaType.DeployArea)
            {
                mainTilemap = areaTM.tilemap;
                break;
            }
        }

        // areaMapの初期化
        InitializeAreaMap();
    }

    private void InitializeAreaMap()
    {
        areaMap.Clear();

        foreach (var areaTM in areatilemaps)
        {
            List<Vector3Int> cells = areaTM.GetUsedCells();
            foreach (var cell in cells)
            {
                areaMap[cell] = areaTM.areaType;
                Debug.Log($"Registered cell {cell} as {areaTM.areaType}");
            }
        }

        Debug.Log("areaMap 初期化完了: " + areaMap.Count + "セル");
    }

    public bool CanPlaceUnit(Vector3Int cell, UnitType unitType)
    {
        if (!areaMap.ContainsKey(cell)) return false;

        AreaType area = areaMap[cell];

        switch (unitType)
        {
            case UnitType.Melee:
                return area == AreaType.DeployArea;

            case UnitType.Ranged:
            case UnitType.Magic:
                return area == AreaType.HighGroundArea;

            default:
                return false;
        }
    }

    private void OnDrawGizmos()
    {
        // ゲーム実行中は描画しない
        if (areaMap == null || mainTilemap == null) return;

        // 各マスの中央座標取得
        foreach (var kvp in areaMap)
        {
            Vector3 worldPos = mainTilemap.GetCellCenterWorld(kvp.Key);
            Color color = Color.white;

            // マスのタイプに応じてGizumoの色を変更
            switch (kvp.Value)
            {
                case AreaType.DeployArea:
                    color = Color.green;
                    break;
                case AreaType.HighGroundArea:
                    color = Color.yellow;
                    break;
                case AreaType.None:
                    color = Color.red;
                    break;
            }

            Gizmos.color = color;
            Gizmos.DrawCube(worldPos, new Vector3(0.9f, 0.9f, 0.1f));
        }
    }
}
