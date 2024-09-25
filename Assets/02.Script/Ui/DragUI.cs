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

/*    public void OnPointerClick(PointerEventData eventData)
    {
        var selectUI = eventData.pointerEnter;

        Debug.Log("Select UI : " + selectUI);

        if (selectUI != null && selectUI.GetComponent<UISkillImage>() != null)
        {
            Debug.Log("Click !");
            isSkillDrag = true;

            ActiveSkill skill = selectUI.GetComponent<UISkillImage>().Skill;
            dragIcon.SetActiveSkill(skill);

            dragIcon.gameObject.SetActive(true);
        }
    }*/

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
            Vector2 tooltipPos = new Vector2(invenSlotUI.transform.position.x + 480f, invenSlotUI.transform.position.y - 125f);
            tooltip.transform.position = tooltipPos;
        }
        else
        {
            invenItem = null;
        }

        if (invenItem != null && string.IsNullOrEmpty(invenItem.ItemName))
        {
            invenItem = null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ui = null;
        isDrag = false;
    }
}
