using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyRouteObject routePrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    private void Update()
    {
        if (!GameManager.Instance.canSpawnEnemy())
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    public void Spawn()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetRoute(routePrefab);

        GameManager.Instance.OnEnemySpawned();
    }
}
