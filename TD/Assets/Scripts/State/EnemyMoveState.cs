using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMoveState : IEnemyUnit
{

    private Vector3 targetPosition;
    private bool hasTarget;

    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");
        SetNextTarget(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        // 移動処理
        // enemy.transform.Translate(enemy.MoveDirection * enemy.MOV * Time.deltaTime);


        if (!hasTarget)
        {
            SetNextTarget(enemy);
        }

        // 次のマスへ移動
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.MOV * Time.deltaTime);

        if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.01f)
        {
            hasTarget = false;
        }

        // 味方が近づいたら攻撃状態に
        if (enemy.IsAllyInRange())
        {
            enemy.ChangeState(new EnemyAttackState());
        }

        // ToDo: 「タイルの端についたら止まる　or 切り返す」などを追加
    }

    public void SetNextTarget(Enemy enemy)
    {
        Tilemap tilemap = enemy.tilemap;
        Vector3 currentPos = enemy.transform.position;
        Vector3Int currentCell = tilemap.WorldToCell(currentPos);

        // 1マス左へ
        Vector3Int nextCell = new Vector3Int(currentCell.x - 1, currentCell.y, currentCell.z);
        targetPosition = tilemap.GetCellCenterWorld(nextCell);
        hasTarget = true;
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態を抜けた");
    }
}

