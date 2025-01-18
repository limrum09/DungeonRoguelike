using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private InvenItem item;

    public InvenItem Item => item;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.pointerEnter.transform.position;
        Manager.Instance.UIAndScene.BuffUI.ViewBuffInfoPanel(item, mousePos.x, mousePos.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Manager.Instance.UIAndScene.BuffUI.HideBuffInfoPanel();
    }

    public void SetBuffIcon(InvenItem getItem)
    {
        item = getItem;

        icon.sprite = item.ItemImage;
    }
}
