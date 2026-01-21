using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    public void UpdateWaveCount(int currentWave, int totalWaves)
    {
        waveText.text = $"Wave: {currentWave} / {totalWaves}";
    }
}
