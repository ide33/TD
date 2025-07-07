using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemyMoveState : IEnemyUnit
{
    private int currentIndex = 0;
    private List<Vector3> path = new List<Vector3>();
    private const float reachThreshold = 0.01f;

    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");

        // 初期位置をタイルにスナップ
        Vector3Int startCell = MapManager.Instance.mainTilemap.WorldToCell(enemy.transform.position);
        enemy.transform.position = MapManager.Instance.mainTilemap.GetCellCenterWorld(startCell);

        // タイルの中心座標に変換
        path.Clear();
        foreach (var cell in enemy.routeAsset.routeCells)
        {
            Vector3 worldPos = MapManager.Instance.mainTilemap.GetCellCenterWorld(cell);
            path.Add(worldPos);
        }

        currentIndex = 0;
        enemy.transform.position = path[0];
    }

    public void UpdateState(Enemy enemy)
    {
        if (path == null || currentIndex >= path.Count) return;

        Vector3 target = path[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.MOV * Time.deltaTime);

        // 目標地点に近づいたら次へ
        if (Vector3.Distance(enemy.transform.position, target) < reachThreshold)
        {
            currentIndex++;

            // 最後の地点に着いた
            if (currentIndex >= path.Count)
            {
                Debug.Log($"{enemy.name}がルートの終点に到達しました");
                return;
            }
        }

        // 味方が近くにいる場合、攻撃状態に遷移
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

