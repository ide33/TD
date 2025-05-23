using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    public TMP_Text costText;

    private void Update()
    {
        // 現在のコストを表示
        costText.text = "Cost:" + CostManager.Instance.CurrentCost;
    }
}
