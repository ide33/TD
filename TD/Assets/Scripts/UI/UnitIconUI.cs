using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UnitIconUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public DeployableUnitData unitData;

    [SerializeField] private  TextMeshProUGUI costText;

    private Image image;


    private void Start()
    {
        image = GetComponent<Image>();

        if (image == null)
        {
            Debug.LogError("Imageコンポーネントがありません", this);
            return;
        }

        if (unitData == null)
        {
            Debug.LogError("unitData が設定されていません", this);
            return;
        }

        if (unitData.icon == null)
        {
            Debug.LogError("unitData.icon が設定されていません", unitData);
            return;
        }


        image.sprite = unitData.icon;

        if (costText != null)
        {
            costText.text = unitData.cost.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグを開始
        DragManager.Instance.BeginDrag(unitData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中、ゴースト表示
        DragManager.Instance.UpdateDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグ終了、ユニット配置
        DragManager.Instance.EndDrag();
    }
}
