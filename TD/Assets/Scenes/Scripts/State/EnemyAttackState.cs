using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : IEnemyUnit
{
    // 攻撃のクールダウン   
    private float attackCooldown = 1.5f; 

    // タイマー変数
    private float timer;

    public void EnterState(Enemy enemy)
    {
        Debug.Log("敵:攻撃状態に入りました");

        // タイマーを攻撃クールダウンに初期化
        timer = attackCooldown;
    }

    public void UpdateState(Enemy enemy)
    {
        // 時間経過に応じてタイマーを減らす
        timer -= Time.deltaTime;

        // タイマーが0以下になったら攻撃可能
        if (timer <= 0f)
        {
            // 範囲内の敵を検出
            Collider2D hit = Physics2D.OverlapCircle(enemy.AttackPoint.position, enemy.attackRange, LayerMask.GetMask("Ally"));


            if (hit != null)
            {
                Debug.Log("敵:攻撃開始");

                // 味方のUnitBaseを取得、ダメージを与える
                UnitBase ally = hit.GetComponent<UnitBase>();

                if (ally != null)
                {
                    ally.TakeDamage(10);
                }

                // タイマーをリセット
                timer = attackCooldown;
            }
            else if (!enemy.IsAllyInRange())
            {
                // 味方がいなければ攻撃をやめて待機状態へ
                enemy.ChangeState(new EnemyMoveState());
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("敵:攻撃状態を抜けます");
    }
}
