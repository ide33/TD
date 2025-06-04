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
        // コストをAllyDataから取得
        Ally allyComponent = data.unitprefab.GetComponent<Ally>();

        int cost = allyComponent.CST;

        if (!CostManager.Instance.TrySpendCost(cost))
        {
            Debug.Log("コストが足りません");
            return;
        }

        // ユニットプレハブを仮生成、ゴーストとする
        draggingData = data;
        ghost = Instantiate(data.unitprefab);

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
            // 配置可能エリアを検出
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 1f, LayerMask.GetMask("DeployArea"));

            if (hit.collider != null)
            {
                // 実体を配置
                Instantiate(draggingData.unitprefab, ghost.transform.position, Quaternion.identity);
            }

            // ゴーストを削除
            Destroy(ghost);
            ghost = null;
            draggingData = null;
        }
    }
}
