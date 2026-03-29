using TMPro;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baseText;

    public void UpdateBaseHP(int currentHp, int maxHp)
    {
        baseText.text = $"Base HP: {currentHp} / {maxHp}";
    }
}
