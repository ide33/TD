using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    //コストを表示するTextMeshPro
    public TMP_Text costText;

    private void Start()
    {
        // ゲーム開始時、コスト変更のイベントに登録
        CostManager.Instance.OnCostChanged += UpdateCostDisPlay;

        // 最初に現在のコスト表示
        UpdateCostDisPlay(CostManager.Instance.CurrentCost);   
    }

    private void OnDestroy()
    {
        // オブジェクト削除時にイベント解除
        if (CostManager.Instance != null)
        {
            CostManager.Instance.OnCostChanged -= UpdateCostDisPlay;
        }
    }

    private void UpdateCostDisPlay(int newCost)
    {
        // 現在のコストを表示
        costText.text = $"Cost: {newCost}";
    }
}
