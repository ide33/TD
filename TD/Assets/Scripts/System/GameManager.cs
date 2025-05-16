using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 自陣の耐久値
    public int baseHP = 10;

    // 出現予定の敵総数
    public int totalEnemis = 20;

    // 倒した敵総数
    public int defeatedEnemies = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void DamageBase(int damage)
    {
        baseHP -= damage;

        if (baseHP <= 0)
        {
            LoseGame();
        }
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;

        if (defeatedEnemies >= totalEnemis)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("勝利！");
    }

    private void LoseGame()
    {
        Debug.Log("敗北...");
    }
}
