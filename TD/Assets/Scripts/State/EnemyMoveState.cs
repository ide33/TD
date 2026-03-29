using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemyMoveState : IEnemyUnit
{
    private int currentIndex = 0;
    private Transform[] routePoints;
    private const float reachThreshold = 0.01f;

    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy.name}が移動状態に入った");

        if (enemy.Route == null || enemy.Route.RoutePoints.Length == 0)
        {
            Debug.LogError($"{enemy.name}のルートが設定されていません！");
            return;
        }

        routePoints = enemy.Route.RoutePoints;
        currentIndex = 0;

        enemy.transform.position = routePoints[0].position;
    }

    public void UpdateState(Enemy enemy)
    {
        if (currentIndex >= routePoints.Length) return;

        Transform target = routePoints[currentIndex];

        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            target.position,
            enemy.CurrentMoveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(enemy.transform.position, target.position) < reachThreshold)
        {
            currentIndex++;

            // 最後の地点に着いた
            if (currentIndex >= routePoints.Length)
            {
                Debug.Log($"{enemy.name}がルートの終点に到達しました");
                enemy.ReachGoal();
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

