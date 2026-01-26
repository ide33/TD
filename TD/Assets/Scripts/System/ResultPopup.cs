using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPopup : MonoBehaviour
{
    [SerializeField] private Button titleButton;

    private void Start()
    {
        titleButton.onClick.AddListener(OnTitleButton);
    }

    private void OnTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
