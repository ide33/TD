using UnityEngine;

// 近接攻撃クラス
public class MeleeAttack : IAttackStrategy
{
    private GameObject hitEffectPrefab;

    public MeleeAttack(GameObject hitEffectPrefab)
    {
        this.hitEffectPrefab = hitEffectPrefab;
    }

    public void Attack(UnitBase attacker, UnitBase target)
    {
        GameObject effect = GameObject.Instantiate(
            hitEffectPrefab,
            target.transform.position,
            Quaternion.identity
        );

        // 攻撃力から防御力を引きダメージを与える
        int damage = Mathf.Max(1, attacker.STR - target.DEF);
        target.TakeDamage(damage);

        GameObject.Destroy(effect, 0.5f);

        // // attakerがAllyだったらSP加算
        // if (attacker is Ally ally)
        // {
        //     SkillManager.Instance.AddSP(ally);
        // }
    }
}
