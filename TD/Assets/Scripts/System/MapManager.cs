using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    // MaooMAnagerのシングルトン
    public static MapManager Instance { get; private set; }

    public Tilemap tilemap;
    private Dictionary<Vector3Int, AreaType> areaMap = new Dictionary<Vector3Int, AreaType>();

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

        InitializeAreaMap();
    }

    private void InitializeAreaMap()
    {
        // マップ全体の範囲走査
        for (int x = -11; x <= 10; x++)
        {
            for (int y = -5; y <= 4; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                // HighGroundArea
                if ((x == -10 && y == -3) || (x == -9 && y == -3) || (x == -8 && y == -3) ||  (x == -7 && y == -3) || (x == -6 && y == -3) || (x == -5 && y == -3) || (x == -4 && y == -3) || (x == -3 && y == -3) || (x == -2 && y == -3) || (x == -1 && y == -3) || (x == 0 && y == -3) || (x == 1 && y == -3) || (x == 2 && y == -3) || (x == 3 && y == -3) || (x == 4 && y == -3) || (x == 5 && y == -3) || (x == 6 && y == -3) || (x == 7 && y == -3) || (x == 8 && y == -3) ||(x == 9 && y == -3) ||  // 下側の高台
                (x == -10 && y == -4) || (x == -9 && y == -4) || (x == -8 && y == -4) ||  (x == -7 && y == -4) || (x == -6 && y == -4) || (x == -5 && y == -4) || (x == -4 && y == -4) || (x == -3 && y == -4) || (x == -2 && y == -4) || (x == -1 && y == -4) || (x == 0 && y == -4) || (x == 1 && y == -4) || (x == 2 && y == -4) || (x == 3 && y == -4) || (x == 4 && y == -4) || (x == 5 && y == -4) || (x == 6 && y == -4) || (x == 7 && y == -4) || (x == 8 && y == -4) ||(x == 9 && y == -4) ||
                (x == -9 && y == 3) || (x == -8 && y == 3) ||  (x == -7 && y == 3) || (x == -6 && y == 3) || (x == -5 && y == 3) || (x == -4 && y == 3) || (x == -3 && y == 3) || (x == -2 && y == 3) || (x == -1 && y == 3) || (x == 0 && y == 3) || (x == 1 && y == 3) || (x == 2 && y == 3) || (x == 3 && y == 3) || (x == 4 && y == 3) || (x == 5 && y == 3) || (x == 6 && y == 3) || (x == 7 && y == 3) || (x == 8 && y == 3) ||  // 上側の高台
                (x == -9 && y == 2) || (x == -8 && y == 2) ||  (x == -7 && y == 2) || (x == -6 && y == 2) || (x == -5 && y == 2) || (x == -4 && y == 2) || (x == -3 && y == 2) || (x == -2 && y == 2) || (x == -1 && y == 2) || (x == 0 && y == 2) || (x == 1 && y == 2) || (x == 2 && y == 2) || (x == 3 && y == 2) || (x == 4 && y == 2) || (x == 5 && y == 2) || (x == 6 && y == 2) || (x == 7 && y == 2) || (x == 8 && y == 2) ||
                (x == -2 && y == -2) || (x == -1 && y == -2) || (x == -2 && y == -1) || (x == -1 && y == -1) ||  // 下側の出っ張りの高台
                (x == -9 && y == 0) || (x == -8 && y == 0) || (x == -9 && y == 1) || (x == -8 && y == 1) ||
                (x == 6 && y == 0) || (x == 5 && y == 0) || (x == 6 && y == 1) || (x == 5 && y == 1)) 
                {
                    areaMap[pos] = AreaType.HighGroundArea;
                }

                // None
                else if (y == -5 || y == 4 || x == -11 || x == 10  // 上下左右の端の配置不可エリア
                || y == 3 && x == -10 || y == 2 && x == -10 || y == 1 && x == -10 || y == 0 && x == -10  // 左端の配置不可エリア
                || y == 3 && x == 9 || y == 1 && x == 9 || y == 2 && x == 9 || y == 0 && x == 9)
                {
                    areaMap[pos] = AreaType.None;
                }

                // DeployArea
                else
                {
                    areaMap[pos] = AreaType.DeployArea;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (areaMap == null) return;

        foreach (var kvp in areaMap)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(kvp.Key);

            switch (kvp.Value)
            {
                case AreaType.DeployArea:
                    Gizmos.color = Color.green;
                    break;
                case AreaType.HighGroundArea:
                    Gizmos.color = Color.yellow;
                    break;
                case AreaType.None:
                    Gizmos.color = Color.red;
                    break;
            }

            Gizmos.DrawCube(worldPos, new Vector3(0.9f, 0.9f, 0.1f));
        }
    }
}
