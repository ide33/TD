using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    public void Spawn(EnemyRouteObject route)
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetRoute(route);

        GameManager.Instance.OnEnemySpawned();
    }
}
