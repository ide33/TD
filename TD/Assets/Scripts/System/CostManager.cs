using System;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class CostManager : MonoBehaviour
{
    // CostManagerのシングルトン
    public static CostManager Instance { get; private set; }

    [Header("Cost Settings")]

    // 最大コスト
    public int maxCost = 99;

    // 1秒あたりの回復量
    public float regenRate = 1f;

    // 時間経過での蓄積値
    private float costAccumulator = 0f;

    // 読み取り可能な現在のコスト
    public int CurrentCost { get; private set; } = 0;

    // コスト増減イベント
    public event Action<int> OnCostChanged;

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
        CurrentCost = 0;

        // UIに反映
        OnCostChanged?.Invoke(CurrentCost);
    }

    private void Update()
    {
        // 時間経過でコスト回復
        costAccumulator += regenRate * Time.deltaTime;

        if (costAccumulator >= 1f)
        {
            // 整数分だけ加算
            int regenAmount = Mathf.FloorToInt(costAccumulator);

            // コスト追加
            AddCost(regenAmount);

            // 使用した分減少
            costAccumulator -= regenAmount;
        }
    }

    public bool TrySpendCost(int amount)
    {
        // キャラ配置に必要なコストがあるかどうか
        if (CurrentCost >= amount)
        {
            CurrentCost -= amount;

            // コスト更新
            OnCostChanged?.Invoke(CurrentCost);
            return true;
        }
        // コスト不足
        return false;
    }

    public void AddCost(int amount)
    {
        int previousCost = CurrentCost;

        // 上限を超えないようにコスト加算
        CurrentCost = Mathf.Min(CurrentCost + amount, maxCost);

        // コスト更新
        OnCostChanged?.Invoke(CurrentCost);

        // 値が変わったときだけイベント発火
        if (CurrentCost != previousCost)
            OnCostChanged?.Invoke(CurrentCost);
    }
}
