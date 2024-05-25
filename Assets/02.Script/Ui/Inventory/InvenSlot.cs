using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    public int index;

    public ITEMTYPE itemType;
    public string itemName;
    public int itemCnt;
    public Image itemImage;
    public string itemInfomation;

    [SerializeField]
    private GameObject itemCntTextObject;
    [SerializeField]
    private TextMeshProUGUI itemCntText;

    private bool isView;
    private void Start()
    {
        itemCnt = 0;
        isView = false;
    }

    public void SetSlotIndex(int _index)
    {
        index = _index;
    }

    public void SetSlotItem(InvenItem item)
    {
        if(item != null)
        {
            itemType = item.itemtype;
            itemName = item.itemName;
            itemCnt = item.itemCnt;
            itemImage.sprite = item.itemImage;
            itemInfomation = item.itemInfo;
        }
        
        RefreshSlot();
    }

    public void RemoveSlot()
    {
        itemType = ITEMTYPE.All;
        itemName = "";
        itemCnt = 0;
        itemImage.sprite = null;
        itemInfomation = "";
        RefreshSlot();
    }

    public void ViewAndHideInvenSlot(bool tf)
    {
        Color color = itemImage.GetComponent<Image>().color;
        
        // tf == true, invenslot has item
        if (tf)
        {
            // ∫“≈ı∏Ì
            color.a = 1.0f;

            itemCntTextObject.SetActive(true);
            isView = true;
        }
        else
        {
            color.a = 0.0f;

            itemCntTextObject.SetActive(false);
            isView = false;
        }

        itemImage.GetComponent<Image>().color = color;
        itemCntText.text = itemCnt.ToString();
    }

    public void RefreshSlot()
    {
        if (!isView)
        {
            ViewAndHideInvenSlot(true);
        }

        if (itemCnt >= 1)
        {
            itemCntText.text = itemCnt.ToString();
        }
        else if(itemCnt == 0)
        {
            ViewAndHideInvenSlot(false);            
        }
    }
}
