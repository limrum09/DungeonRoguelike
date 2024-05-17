using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField]
    private GameObject tooltipPanel;
    private InvenSlot invenSlot;
    private Image itemTooltipImage;
    private TextMeshProUGUI itemTooltipName;
    private TextMeshProUGUI itemTooltipInfo;

    public void OnPointerMove(PointerEventData eventData)
    {
        GameObject enterUI = eventData.pointerEnter;

        invenSlot = null;

        if (enterUI != null && enterUI.GetComponent<InvenSlot>())
        {
            invenSlot = enterUI.GetComponent<InvenSlot>();
        }

        Vector2 eventPos = new Vector2(eventData.position.x + 240f, eventData.position.y - 100f);
        tooltipPanel.transform.position = eventPos;
        ViewItemTooltip();
    }

    // Start is called before the first frame update
    void Start()
    {
        tooltipPanel = GameObject.FindGameObjectWithTag("ToolTip").transform.GetChild(0).gameObject;
        itemTooltipImage = tooltipPanel.transform.GetChild(0).GetComponent<Image>();
        itemTooltipName = tooltipPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemTooltipInfo = tooltipPanel.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void ViewItemTooltip()
    {
        if(invenSlot != null && invenSlot.itemName == "")
        {
            tooltipPanel.SetActive(false);
            itemTooltipImage.sprite = null;
            itemTooltipName.text = "";
            itemTooltipInfo.text = "";
        }
        else if(invenSlot != null)
        {
            tooltipPanel.SetActive(true);
            itemTooltipImage.sprite = invenSlot.itemImage.sprite;
            itemTooltipName.text = invenSlot.itemName;
            itemTooltipInfo.text = invenSlot.itemInfomation;
        }        
    }
}
