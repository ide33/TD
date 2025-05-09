using UnityEngine;

public class EnemyMoveState : IEnemyUnit
{
    public void EnterState(Enemy enemy)
    {
        // 状態に入った時
        Debug.Log($"{enemy.name}が移動状態に入った");
    }

    public void UpdateState(Enemy enemy)
    {
        enemy.transform.Translate(enemy.MoveDirection * enemy.MoveSpeed * Time.deltaTime);

        // ToDo: 「タイルの端についたら止まる　or 切り返す」などを追加
    }

    public void ExitState(Enemy enemy)
    {
        // 状態を抜けるときの処理
        Debug.Log($"{enemy.name}が移動状態を抜けた");
    }
}

