using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPopup : MonoBehaviour
{
    [SerializeField] private Button titleButton;
    [SerializeField] private Button nextButton;

    private void Start()
    {
        titleButton.onClick.AddListener(OnTitleButton);
        nextButton.onClick.AddListener(OnNextButton);
    }

    private void OnTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void OnNextButton()
    {
        SceneManager.LoadScene("Level2Scene");
    }
}
