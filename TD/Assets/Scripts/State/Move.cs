using UnityEngine;

public class Move : IEnemyUnit
{
    private EnemyController enemy;

    public Move(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {

    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
        
    }
}
