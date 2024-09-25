using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltipBackGround;
    [SerializeField]
    private Image itemTooltipImage;
    [SerializeField]
    private TextMeshProUGUI itemTooltipName;
    [SerializeField]
    private TextMeshProUGUI itemTooltipInfo;

    private void Start()
    {
        tooltipBackGround.SetActive(false);
    }

    public void ViewItemTooltip(InvenSlot invenSlot)
    {
        InvenSlot item = invenSlot;

        if(item == null)
        {
            HideTooltip();
        }
        else if (item != null && item.ItemName == "")
        {
            HideTooltip();
        }
        else if (item != null)
        {
            tooltipBackGround.SetActive(true);
            itemTooltipImage.sprite = item.ItemImage.sprite;
            itemTooltipName.text = item.ItemName;
            itemTooltipInfo.text = item.ItemInformation;
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
