using UnityEngine;
using TMPro;

public class EnemyStatsUI : MonoBehaviour
{
    [SerializeField] private UnitStats enemyStats;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI strText;
    [SerializeField] private TextMeshProUGUI defText;

    void Start()
    {
        ShowStats();
    }

    public void ShowStats()
    {
        nameText.text = $"EnemyStats";
        hpText.text = $"HP: {enemyStats.maxHP}";
        strText.text = $"STR: {enemyStats.STR}";
        defText.text = $"DEF: {enemyStats.DEF}";
    }

}
