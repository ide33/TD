using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefub;
    [SerializeField] private EnemyRouteObject route;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(enemyPrefub);
        
        // ルート戦闘にスポーン
        enemy.transform.position = route.RoutePoints[0].position;
    }
}
