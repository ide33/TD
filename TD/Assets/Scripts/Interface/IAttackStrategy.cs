using UnityEngine;

public interface IAttackStrategy
{
    // 攻撃方法のインターフェイス
    void Attack(UnitBase attacker, UnitBase target);
}
