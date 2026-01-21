using UnityEngine;

[System.Serializable]
public class WaveData : MonoBehaviour
{
   public int enemyCount;
    public float spawnInterval;

    [SerializeField] private WaveData[] waves;
}
