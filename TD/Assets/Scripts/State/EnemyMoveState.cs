using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMoveState : IEnemyUnit
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");

        enemy.spawnCell = MapManager.Instance.mainTilemap.WorldToCell(enemy.transform.position);
        enemy.transform.position = MapManager.Instance.mainTilemap.GetCellCenterWorld(enemy.spawnCell);

        // 自陣(ゴール)の場所
        enemy.goalCell = new Vector3Int(-10, -1, 0);
    }

    public void UpdateState(Enemy enemy)
    {
        // 常に左へ移動
        enemy.transform.position += Vector3.left * enemy.MOV * Time.deltaTime;

        // 味方が近づいたら攻撃状態に
        if (enemy.IsAllyInRange())
        {
            enemy.ChangeState(new EnemyAttackState());
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態を抜けた");
    }
}

