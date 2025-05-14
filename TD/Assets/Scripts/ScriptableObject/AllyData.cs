using UnityEngine;

[CreateAssetMenu(fileName = "AllyData", menuName = "Unit/AllyData", order = 1)]
public class AllyData : UnitData
{
    // ブロック数
    public int BLK;

    // スキルポイント
    public float SP;

    // 攻撃範囲
    public float attackRange;
}
