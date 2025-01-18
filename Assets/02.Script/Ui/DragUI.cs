using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler, IDragHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private ToolTipController tooltipController;

    GameObject ui;
    private bool isDrag;

    Vector2 startinPoint;
    Vector2 clickPoint;
    Vector2 offset;

    private InvenSlot invenItem;
    private bool isSkillDrag;

    private void Start()
    {
        invenItem = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            offset = eventData.position - clickPoint;
            ui.transform.position = startinPoint + offset;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        var selectUI = eventData.pointerEnter;

        if (selectUI.transform.gameObject.CompareTag("GameUI"))
        {
            selectUI.transform.parent.SetAsLastSibling();
        }

        if (!isDrag && selectUI != null && selectUI.CompareTag("DragUI"))
        {
            ui = selectUI.transform.parent.gameObject;
            int index = ui.transform.childCount - 1;
            ui.transform.parent.SetSiblingIndex(index);

            startinPoint = ui.transform.position;
            clickPoint = eventData.position;

            isDrag = true;
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        var onUI = eventData.pointerEnter;

        if (onUI != null && onUI.gameObject.GetComponent<InvenSlot>())
        {
            GameObject invenSlotUI = onUI.gameObject;
            invenItem = invenSlotUI.GetComponent<InvenSlot>();
            
            tooltipController.ViewItemTooltip(invenItem);
            Vector2 tooltipPos = new Vector2(invenSlotUI.transform.position.x + 240f, invenSlotUI.transform.position.y - 65f);
            tooltip.transform.position = tooltipPos;
        }
        else
        {
            invenItem = null;
            tooltipController.ViewItemTooltip(invenItem);
        }

        if (invenItem != null && string.IsNullOrEmpty(invenItem.ItemName))
        {
            invenItem = null;
            tooltipController.ViewItemTooltip(invenItem);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ui = null;
        isDrag = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        invenItem = null;
        tooltipController.ViewItemTooltip(invenItem);
    }
}
