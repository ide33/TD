using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMoveState : IEnemyUnit
{

    private Vector3 targetPosition;
    // private bool hasTarget;

    public Vector3Int spawnCell;
    public Vector3Int goalCell;

    public List<Vector3Int> movePath;
    public int currentPathIndex = 0;

    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");

        Debug.Log($"spawnCell: {enemy.spawnCell}, goalCell: {enemy.goalCell}");

        enemy.spawnCell = MapManager.Instance.tilemap.WorldToCell(enemy.transform.position);
        enemy.goalCell = new Vector3Int(-10, -1, 0); // 青い三角のセル

        // PathFinderからルートを取得
        List<Vector3Int> cellPath = PathFinder.Instance.FindPath(enemy.spawnCell, enemy.goalCell);

        if (cellPath == null || cellPath.Count == 0)
        {
            Debug.Log("経路が見つからなかった");
            return;
        }

        enemy.movePath = new List<Vector3>();
        foreach (var cell in cellPath)
        {
            enemy.movePath.Add(enemy.tilemap.GetCellCenterWorld(cell));
        }

        enemy.currentPathIndex = 0;

        // SetNextTarget(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        // 移動処理
        // enemy.transform.Translate(enemy.MoveDirection * enemy.MOV * Time.deltaTime);

        if (enemy.movePath == null || enemy.currentPathIndex >= enemy.movePath.Count) return;

        Vector3 target = enemy.movePath[enemy.currentPathIndex];

        // 次のマスへ移動
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.MOV * Time.deltaTime);

        if (Vector3.Distance(enemy.transform.position, target) < 0.01f)
        {
            enemy.currentPathIndex++;
        }


        // if (!hasTarget)
        // {
        //     SetNextTarget(enemy);
        // }

        if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.01f)
        {
            enemy.currentPathIndex++;
        }

        // 味方が近づいたら攻撃状態に
        if (enemy.IsAllyInRange())
        {
            enemy.ChangeState(new EnemyAttackState());
        }
    }

    // public void SetNextTarget(Enemy enemy)
    // {
    //     Tilemap tilemap = enemy.tilemap;
    //     Vector3 currentPos = enemy.transform.position;
    //     Vector3Int currentCell = tilemap.WorldToCell(currentPos);

    //     // 1マス左へ
    //     Vector3Int nextCell = new Vector3Int(currentCell.x - 1, currentCell.y, currentCell.z);
    //     targetPosition = tilemap.GetCellCenterWorld(nextCell);
    //     hasTarget = true;
    // }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態を抜けた");
    }
}

