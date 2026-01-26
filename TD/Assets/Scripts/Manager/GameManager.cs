using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManagerシングルトン
    public static GameManager Instance { get; private set; }

    [SerializeField] private BaseUI baseUI;
    [SerializeField] private DefeatEnemyUI defeatEnemyUI;

    [SerializeField] private WaveManager waveManager;

    [SerializeField] private GameObject gameClearPopupPrefab;
    [SerializeField] private GameObject gameOverPopupPrefab;


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

    private void Start()
    {
        baseUI.UpdateBaseHP(baseHP, 10);
        defeatEnemyUI.UpdateDefeatEnemyCount(defeatedEnemies, totalEnemies);
    }

    public void OnEnemySpawned()
    {
        // 敵が出現したときの処理
        Debug.Log("敵が出現しました。");
    }

    public void EnemyDefeated()
    {
        // 撃破数をカウント
        defeatedEnemies++;
        defeatEnemyUI.UpdateDefeatEnemyCount(defeatedEnemies, totalEnemies);
    }

    public void DamageBase(int damage)
    {
        // 自陣にダメージ
        baseHP -= damage;
        baseHP = Mathf.Max(0, baseHP);

        baseUI.UpdateBaseHP(baseHP, 10);

        if (baseHP <= 0)
        {
            LoseGame();
        }
    }

    public void OnAllEnemiesCleared()
    {
        WinGame();
    }

    private void WinGame()
    {
        Debug.Log("勝利！");
        PopupManager.Instance.ShowGameClear();
    }

    private void LoseGame()
    {
        Debug.Log("敗北...");
        PopupManager.Instance.ShowGameOver();
    }
}
