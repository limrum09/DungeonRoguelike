using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler, IDragHandler
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
        var moveUI = eventData.pointerEnter;


        if (moveUI != null && moveUI.gameObject.GetComponent<InvenSlot>())
        {
            invenItem = moveUI.gameObject.GetComponent<InvenSlot>();
        }
        else
        {
            invenItem = null;
        }

        if(invenItem != null && string.IsNullOrEmpty(invenItem.itemName))
        {
            invenItem = null;
        }

        tooltipController.ViewItemTooltip(invenItem);
        Vector2 eventPos = new Vector2(eventData.position.x + 240f, eventData.position.y - 100f);
        tooltip.transform.position = eventPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ui = null;
        isDrag = false;
    }
}
