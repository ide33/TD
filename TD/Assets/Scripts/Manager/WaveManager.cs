using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [SerializeField] private WaveData[] waves;
    [SerializeField] private EnemySpawner enemySpawner;

    private int currentWaveIndex = 0;
    private int aliveEnemyCount = 0;
    private bool isSpawning = false;

    public bool IsAllWavesFinished =>
        currentWaveIndex >= waves.Length && aliveEnemyCount <= 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            yield break;
        }

        isSpawning = true;

        WaveData wave = waves[currentWaveIndex];
        Debug.Log($"Wave {currentWaveIndex + 1} 開始: 敵数 {wave.enemyCount}");

        RouteLineDrawer drawer = wave.route.GetComponent<RouteLineDrawer>();
        if (drawer != null)
            drawer.ShowRoute(3f);

        for (int i = 0; i < wave.enemyCount; i++)
        {
            enemySpawner.Spawn(wave.route);
            aliveEnemyCount++;

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        currentWaveIndex++;
        isSpawning = false;
    }

    public void OnEnemyRemoved()
    {
        aliveEnemyCount = Mathf.Max(0, aliveEnemyCount - 1);

        if (IsAllWavesFinished)
        {
            GameManager.Instance.OnAllEnemiesCleared();
        }
        else if (!isSpawning && aliveEnemyCount <= 0)
        {
            StartCoroutine(StartNextWave());
        }
    }
}
