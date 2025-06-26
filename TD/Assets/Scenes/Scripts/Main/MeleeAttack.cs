using UnityEngine;

// 近接攻撃クラス
public class MeleeAttack : IAttackStrategy
{
    public void Attack(UnitBase attacker, UnitBase target)
    {
        // 攻撃力から防御力を引きダメージを与える
        int damage = Mathf.Max(1, attacker.STR - target.DEF);
        target.TakeDamage(damage);
    }
}
