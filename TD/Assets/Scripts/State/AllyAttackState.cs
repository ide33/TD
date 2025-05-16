using UnityEngine;

public class AllyAttackState : IAllyUnit
{
    // 攻撃のクールタイム
    private float attackCooldown = 1.0f;

    // タイマー変数
    private float timer;

    public void EnterState(Ally ally)
    {
        Debug.Log("味方:攻撃状態に入りました");

        // タイマーを攻撃クールダウンに初期化
        timer = attackCooldown;
    }

    public void UpdateState(Ally ally)
    {
        // 時間経過に応じてタイマーを減らす
        timer -= Time.deltaTime;

        // タイマーが0以下になったら攻撃可能
        if (timer <= 0f)
        {
            // 範囲内の敵を検出
            Collider2D hit = Physics2D.OverlapCircle(ally.AttackPoint.position, ally.attackRange, LayerMask.GetMask("Enemy"));


            if (hit != null)
            {
                Debug.Log("味方:攻撃開始");

                // 敵のUnitBaseを取得、ダメージを与える
                UnitBase enemy = hit.GetComponent<UnitBase>();

                if (enemy != null)
                {
                    ally.attackStrategy.Attack(ally, enemy);
                }

                // タイマーをリセット
                timer = attackCooldown;
            }
            else
            {
                // 敵がいなければ攻撃をやめて待機状態へ
                ally.ChangeState(new AllyIdleState());
            }
        }
    }

    public void ExitState(Ally ally)
    {
        Debug.Log("味方:攻撃状態を抜けます");
    }
}
