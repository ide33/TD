using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Unit/EnemyData", order = 2)]
public class EnemyData : UnitData
{
    // 重量
    public int WGT;

    // 移動速度
    public float MOV;

    // 攻撃範囲
    public float attackRange;
}
