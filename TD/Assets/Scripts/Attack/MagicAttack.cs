using UnityEngine;

public class MagicAttack : IAttackStrategy
{
    private GameObject effectPrefab;

    public MagicAttack(GameObject effectPrefab)
    {
        this.effectPrefab = effectPrefab;
    }
    
    public void Attack(UnitBase attacker, UnitBase target)
    {
        if (effectPrefab == null)
        {
            Debug.LogError("MagicAttack: effectPrefab が null です");
            return;
        }

        // 魔法攻撃力から魔法防御力を引きダメージを与える
        int damage = Mathf.Max(1, attacker.INT - target.RES);
        target.TakeDamage(damage);

        Object.Instantiate(effectPrefab, target.transform.position, Quaternion.identity);

        // // attakerがAllyだったらSP加算
        // if (attacker is Ally ally)
        // {
        //     SkillManager.Instance.AddSP(ally);
        // }
    }
}
