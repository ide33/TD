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

            if (MapManager.Instance.areaMap.TryGetValue(cellPos, out MapManager.AreaType areaType))
            {
                bool CanDeploy = false;

                // ユニットのタイプに応じた配置可否判定
                switch (draggingData.allyData.attackType)
                {
                    case AllyData.AttackType.Melee:
                        CanDeploy = areaType == MapManager.AreaType.DeployArea;
                        break;

                    case AllyData.AttackType.Ranged:
                    case AllyData.AttackType.Magic:
                        CanDeploy = areaType == MapManager.AreaType.HighGroundArea;
                        break;
                }

                if (CanDeploy)
                {
                    if (TryPlaceUnit(snappedPos))
                    {
                        Debug.Log("ユニット配置成功");
                    }
                    else
                    {
                        Debug.Log("この場所には配置できません");
                    }
                }
                else
                {
                    Debug.Log("配置エリア外です");
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

        // 実体を配置
        Instantiate(draggingData.unitprefab, position, Quaternion.identity);
        Debug.Log("ユニット配置");

        return true;
    }
}
