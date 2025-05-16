using UnityEngine;

public class EnemyMoveState : IEnemyUnit
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");
    }

    public void UpdateState(Enemy enemy)
    {
        // 移動処理
        enemy.transform.Translate(enemy.MoveDirection * enemy.MOV * Time.deltaTime);

        // 味方が近づいたら攻撃状態に
        if (enemy.IsAllyInRange())
        {
            enemy.ChangeState(new EnemyAttackState());
        }

        // ToDo: 「タイルの端についたら止まる　or 切り返す」などを追加
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態を抜けた");
    }
}

