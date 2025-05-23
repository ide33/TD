using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManagerシングルトン
    public static GameManager Instance { get; private set; }

    // 自陣の耐久値
    public int baseHP = 10;

    // 出現予定の敵総数
    public int totalEnemies = 20;

    // 倒した敵総数
    public int defeatedEnemies = 0;

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

    public void DamageBase(int damage)
    {
        // 自陣にダメージ
        baseHP -= damage;

        if (baseHP <= 0)
        {
            // 負け
            LoseGame();
        }
    }

    public void EnemyDefeated()
    {
        // 撃破数をカウント
        defeatedEnemies++;

        if (defeatedEnemies >= totalEnemies)
        {
            // 勝ち
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
