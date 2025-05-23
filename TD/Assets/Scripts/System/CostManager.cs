using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class CostManager : MonoBehaviour
{
    // CostManagerのシングルトン
    public static CostManager Instance { get; private set; }

    [Header("Cost Settings")]

    // 最大コスト
    public int maxCost = 0;

    // 1秒あたりの回復量
    public float regenRate = 1f;

    private float costAccumulator = 0f;

    public int CurrentCost { get; private set; }

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
        // コストを初期化
        CurrentCost = maxCost;
    }

    private void Update()
    {
        // 時間経過でコスト回復
        costAccumulator += regenRate * Time.deltaTime;

        if (costAccumulator >= 1f)
        {
            int regenAmount = Mathf.FloorToInt(costAccumulator);
            AddCost(regenAmount);
            costAccumulator -= regenAmount;
        }
    }

    public bool TrySpendCost(int amount)
    {
        if (CurrentCost >= amount)
        {
            CurrentCost -= amount;
            return true;
        }
        return false;
    }

    public void AddCost(int amount)
    {
        CurrentCost = Mathf.Min(CurrentCost + amount, maxCost);
    }
}
