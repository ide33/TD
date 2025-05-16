using UnityEngine;

public class MagicAttack : IAttackStrategy
{
    public void Attack(UnitBase attacker, UnitBase target)
    {
        // 術攻撃力から術防御力を引きダメージを与える
        int damage = Mathf.Max(1, attacker.INT - target.RES);
        target.TakeDamage(damage);
    }
}
