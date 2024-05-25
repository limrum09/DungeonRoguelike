using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject tooltipBackGround;
    [SerializeField]
    private Image itemTooltipImage;
    [SerializeField]
    private TextMeshProUGUI itemTooltipName;
    [SerializeField]
    private TextMeshProUGUI itemTooltipInfo;
    private InvenSlot invenSlot;

    private bool isPointerInside = false;

    public void OnPointerMove(PointerEventData eventData)
    {
        GameObject enterUI = eventData.pointerEnter;

        invenSlot = null;

        if (enterUI != null && enterUI.GetComponent<InvenSlot>())
        {
            invenSlot = enterUI.GetComponent<InvenSlot>();
        }

        Vector2 eventPos = new Vector2(eventData.position.x + 240f, eventData.position.y - 100f);
        tooltipBackGround.transform.position = eventPos;
        ViewItemTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        tooltipBackGround.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
        HideTooltip();
    }

    private void ViewItemTooltip()
    {
        if(invenSlot != null && invenSlot.itemName == "")
        {
            HideTooltip();
        }
        else if(invenSlot != null)
        {
            tooltipBackGround.SetActive(true);
            itemTooltipImage.sprite = invenSlot.itemImage.sprite;
            itemTooltipName.text = invenSlot.itemName;
            itemTooltipInfo.text = invenSlot.itemInfomation;
        }        
    }

    private void HideTooltip()
    {
        tooltipBackGround.SetActive(false);
        itemTooltipImage.sprite = null;
        itemTooltipName.text = "";
        itemTooltipInfo.text = "";
    }
}
