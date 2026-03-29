using UnityEngine;

// 遠距離攻撃クラス
public class RangedAttack : IAttackStrategy
{
    private GameObject projectilePrefab;

    public RangedAttack(GameObject projectilePrefab)
    {
        this.projectilePrefab = projectilePrefab;
    }

    public void Attack(UnitBase attacker, UnitBase target)
    {
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
            // 攻撃力から防御力を引きダメージを与える
            int damage = Mathf.Max(1, attacker.STR - target.DEF);
            target.TakeDamage(damage);
        });

        // // attakerがAllyだったらSP加算
        // if (attacker is Ally ally)
        // {
        //     SkillManager.Instance.AddSP(ally);
        // }
    }
}
