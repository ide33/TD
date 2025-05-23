using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIconUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ユニットデータ
    public DeployableUnitData unitData;

    // 画像
    private Image image;

    private void Start()
    {
        // Imageコンポーネントを取得、表示する画像をunitDataに設定
        image = GetComponent<Image>();
        image.sprite = unitData.icon;
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
