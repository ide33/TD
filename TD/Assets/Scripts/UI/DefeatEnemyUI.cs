using TMPro;
using UnityEngine;

public class DefeatEnemyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI defeatEnemyText;

    public void UpdateDefeatEnemyCount(int current, int total)
    {
        defeatEnemyText.text = $"Defeated Enemies: {current} / {total}";
    }
}
