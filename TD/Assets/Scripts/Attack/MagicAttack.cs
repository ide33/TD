using UnityEngine;

public class MagicAttack : IAttackStrategy
{
    // private GameObject effectPrefab;
    private GameObject projectilePrefab;

    public MagicAttack(GameObject projectilePrefab)
    {
        // this.effectPrefab = effectPrefab;
        this.projectilePrefab = projectilePrefab;
    }

    public void Attack(UnitBase attacker, UnitBase target)
    {
        // if (projectilePrefab == null)
        // {
        //     Debug.LogError("MagicAttack: projectilePrefab が null です");
        //     return;
        // }

        GameObject proj = Object.Instantiate(projectilePrefab, attacker.transform.position, Quaternion.identity);

        // Visualize 初期化
        var visualize = proj.GetComponent<AttackVisualize>();

        if (visualize == null)
        {
            Debug.LogError("AttackVisualize が projectilePrefab に付いていません");
            return;
        }

        visualize.Initialize(target, () =>
        {
            // 魔法攻撃力から魔法防御力を引きダメージを与える
            int damage = Mathf.Max(1, attacker.INT - target.RES);
            target.TakeDamage(damage);
        });

        // Object.Instantiate(projectilePrefab, target.transform.position, Quaternion.identity);

        // // attakerがAllyだったらSP加算
        // if (attacker is Ally ally)
        // {
        //     SkillManager.Instance.AddSP(ally);
        // }
    }
}
