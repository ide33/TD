using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMoveState : IEnemyUnit
{

    private Vector3 targetPosition;
    private bool hasTarget;

    // public Vector3Int spawnCell;
    // public Vector3Int goalCell;

    // public List<Vector3Int> movePath;
    // public int currentPathIndex = 0;

    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");

        // Debug.Log($"spawnCell: {enemy.spawnCell}, goalCell: {enemy.goalCell}");

        enemy.spawnCell = MapManager.Instance.mainTilemap.WorldToCell(enemy.transform.position);
        enemy.transform.position = MapManager.Instance.mainTilemap.GetCellCenterWorld(enemy.spawnCell);
        enemy.goalCell = new Vector3Int(-10, -1, 0); // 青い三角のセル

        hasTarget = false;

        // PathFinderからルートを取得
        // List<Vector3Int> cellPath = PathFinder.Instance.FindPath(enemy.spawnCell, enemy.goalCell);

        // if (cellPath == null || cellPath.Count == 0)
        // {
        //     Debug.Log("経路が見つからなかった");
        //     return;
        // }

        // enemy.movePath = new List<Vector3>();
        // foreach (var cell in cellPath)
        // {
        //     enemy.movePath.Add(enemy.tilemap.GetCellCenterWorld(cell));
        // }

        // enemy.currentPathIndex = 0;

        SetNextTarget(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        // 移動処理
        // enemy.transform.Translate(enemy.MoveDirection * enemy.MOV * Time.deltaTime);

        // if (enemy.movePath == null || enemy.currentPathIndex >= enemy.movePath.Count) return;

        // Vector3 target = enemy.movePath[enemy.currentPathIndex];

        if (!hasTarget) return;

        // 次のマスへ移動
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.MOV * Time.deltaTime);

        if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.01f)
        {
            // 次のセルへ進む
            Vector3Int currentCell = MapManager.Instance.mainTilemap.WorldToCell(enemy.transform.position);
            // enemy.currentPathIndex++;

            // 自陣についたら停止
            if (currentCell == enemy.goalCell)
            {
                Debug.Log($"{enemy.name}は自陣に到達しました");
                return;
            }

            SetNextTarget(enemy);
        }


        // if (!hasTarget)
        // {
        //     SetNextTarget(enemy);
        // }

        // if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.01f)
        // {
        //     enemy.currentPathIndex++;
        // }

        // 味方が近づいたら攻撃状態に
        if (enemy.IsAllyInRange())
        {
            enemy.ChangeState(new EnemyAttackState());
        }
    }

    public void SetNextTarget(Enemy enemy)
    {
        Tilemap tilemap = MapManager.Instance.mainTilemap;

        // Vector3 currentPos = enemy.transform.position;
        Vector3Int currentCell = tilemap.WorldToCell(enemy.transform.position);

        // 1マス進む
        Vector3Int? nextCell = FindNextDeployCell(currentCell);

        // 次のセルがDeployAreaか確認
        if (nextCell.HasValue)
        {
            targetPosition = tilemap.GetCellCenterWorld(nextCell.Value);
            hasTarget = true;
        }
        else
        {
            Debug.Log($"{enemy.name}は進行方向にDeployAreaがありませんでした");
            hasTarget = false;
        }
    }

    private Vector3Int? FindNextDeployCell(Vector3Int currentCell)
    {
        Vector3Int nextCell = new Vector3Int(currentCell.x - 1, currentCell.y, currentCell.z);

        Debug.Log($"現在位置: {currentCell}, 次のセル: {nextCell}");

        if (MapManager.Instance.areaMap.TryGetValue(nextCell, out MapManager.AreaType areaType))
        {
            Debug.Log($"次のセルのタイプ: {areaType}");

            if (areaType == MapManager.AreaType.DeployArea)
            {
                return nextCell;
            }
            else
            {
                Debug.Log($"次のセルは DeployArea ではなく {areaType} でした");
            }
        }
        else
        {
            Debug.Log($"次のセル {nextCell} は areaMap に存在しません");
        }

        return null;
        //     Vector3Int[] directions = new[]
        //     {
        //     Vector3Int.left,
        //     Vector3Int.down,
        //     Vector3Int.up,
        //     Vector3Int.right
        // };

        //     foreach (var dir in directions)
        //     {
        //         Vector3Int next = fromCell + dir;

        //         if (MapManager.Instance.areaMap.TryGetValue(next, out var type)
        //             && type == MapManager.AreaType.DeployArea)
        //         {
        //             return next;
        //         }
        //     }

        //     return null;
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態を抜けた");
    }
}

