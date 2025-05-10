using UnityEngine;

public class AllyIdleState : IAllyUnit
{
    public void EnterState(Ally ally)
    {
        Debug.Log("味方:待機状態に入りました");
    }

    public void UpdateState(Ally ally)
    {
        if (ally.IsEnemyInRange())
        {
            ally.ChangeState(new AllyAttackState());  // 敵が近づいたら攻撃状態に
        }
    }

    public void ExitState(Ally ally)
    {
        Debug.Log("味方:待機状態を抜けます");
    }
}
