using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;

    [Header("Result Popups")]
    [SerializeField] private GameObject gameClearPopupPrefab;
    [SerializeField] private GameObject gameOverPopupPrefab;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowGameClear()
    {
        Instantiate(gameClearPopupPrefab, canvas.transform);
    }

    public void ShowGameOver()
    {
        Instantiate(gameOverPopupPrefab, canvas.transform);
    }
}
