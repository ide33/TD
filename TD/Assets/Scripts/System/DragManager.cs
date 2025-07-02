using UnityEngine;
using UnityEngine.Tilemaps;

public class DragManager : MonoBehaviour
{
    // DragManagerのシングルトン
    public static DragManager Instance { get; private set; }

    // ドラッグ中のゴーストオブジェクト
    private GameObject ghost;

    // ドラッグ中のユニットデータ
    private DeployableUnitData draggingData;

    public Tilemap tilemap;

    private void Awake()
    {
        // 自分自身をInstanceに登録
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void BeginDrag(DeployableUnitData data)
    {
        // ユニットプレハブを仮生成、ゴーストとする
        draggingData = data;

        // ghostPrefabを使用
        GameObject prefabToUse = data.ghostprefab != null ? data.ghostprefab : data.unitprefab;

        ghost = Instantiate(prefabToUse);

        // 半透明
        ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public void UpdateDrag()
    {
        if (ghost != null)
        {
            // マウス座標をワールド座標に変換、ゴーストを追従

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);
            ghost.transform.position = tilemap.GetCellCenterWorld(cellPos);
        }
    }

    public void EndDrag()
    {
        if (ghost != null)
        {
            // 今後タイルマップ準拠に修正
            // 配置可能エリアを検出

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);
            Vector3 snappedPos = tilemap.GetCellCenterWorld(cellPos);
           
            Collider2D hit = Physics2D.OverlapPoint(snappedPos, LayerMask.GetMask("DeployArea", "DeployHigh"));

            if (hit != null)
            {
                IDeployArea area = hit.GetComponent<IDeployArea>();

                if (area != null && !area.IsOccupied && area.CanDeploy(draggingData))
                {
                    // 配置
                    if (TryPlaceUnit(snappedPos))
                    {
                        area.IsOccupied = true;
                    }
                }
                else
                {
                    Debug.Log("ユニット配置済み");
                }
            }

            // ゴーストを削除
            Destroy(ghost);
            ghost = null;
            draggingData = null;
        }
    }

    private bool TryPlaceUnit(Vector2 position)
    {
        // コストをDeployableUnitDataから取得
        int cost = draggingData.cost;

        if (!CostManager.Instance.TrySpendCost(cost))
        {
            Debug.Log("ユニット配置不可");
            return false;
        }

        // 配置マスのセルを取得
        Vector3Int cell = tilemap.WorldToCell(position);

        // 配置可能かチェック
        if (!MapManager.Instance.CanPlaceUnit(cell, draggingData.unitType))
        {
            Debug.Log($"ユニットタイプ{draggingData.unitType}はこのエリアに配置できません");
            return false;
        }
        else
        {
            // 実体を配置
            Instantiate(draggingData.unitprefab, position, Quaternion.identity);
            Debug.Log("ユニット配置");

            return true;
        }
    }
}
