using UnityEngine;

[CreateAssetMenu(fileName = "AllyData", menuName = "Unit/AllyData", order = 1)]
public class AllyData : UnitData
{
    // 攻撃方法
    public enum AttackType { Melee, Ranged, Magic }
    public AttackType attackType;

    // ブロック数
    public int BLK;
    
    // コスト
    public int cost;

    // スキルポイント
    public float SP;

    // 攻撃範囲
    public float attackRange;
}
