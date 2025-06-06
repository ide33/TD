using UnityEngine;

public class DragManager : MonoBehaviour
{
    // DragManagerのシングルトン
    public static DragManager Instance { get; private set; }

    // ドラッグ中のゴーストオブジェクト
    private GameObject ghost;

    // ドラッグ中のユニットデータ
    private DeployableUnitData draggingData;

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
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ghost.transform.position = worldPos;
        }
    }

    public void EndDrag()
    {
        if (ghost != null)
        {
            // 今後タイルマップ準拠に修正
            // 配置可能エリアを検出
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 1f, LayerMask.GetMask("DeployArea"));

            if (hit.collider != null)
            {
                DeployArea area = hit.collider.GetComponent<DeployArea>();

                if (area != null && !area.isOccupied)
                {
                    // 配置
                    Vector3 placePosiotion = hit.collider.transform.position;
                    if (TryPlaceUnit(placePosiotion))
                    {
                        area.isOccupied = true;
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

        if (CostManager.Instance.TrySpendCost(cost))
        {
            // 実体を配置
            Instantiate(draggingData.unitprefab, position, Quaternion.identity);
            Debug.Log("ユニット配置");

            return true;
        }
        else
        {
            Debug.Log("ユニット配置不可");
            return false;
        }
    }
}
