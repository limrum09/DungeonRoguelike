using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    public int index;
    [SerializeField]
    private InvenItem currentItem;

    [Header("Item Info")]
    [SerializeField]
    private ITEMTYPE itemType;
    [SerializeField]
    private string itemCode;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int itemCnt;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private string itemInformation;

    [Header("Slot Object")]
    [SerializeField]
    private GameObject itemCntTextObject;
    [SerializeField]
    private TextMeshProUGUI itemCntText;

    public InvenItem CurrentItem => currentItem;
    public ITEMTYPE ItemType => itemType;
    public string ItemCode => itemCode;
    public string ItemName => itemName;
    public int ItemCnt => itemCnt;
    public Image ItemImage => itemImage;
    public string ItemInformation => itemInformation;

    private bool isCntView;
    private bool isView;
    private void Start()
    {
        itemCnt = 0;
        isView = false;
        isCntView = false;
    }

    public void SetSlotIndex(int _index)
    {
        index = _index;
    }

    public void SetSlotItem(InvenItem item)
    {
        if(item != null)
        {
            currentItem = item;
            itemType = item.itemtype;
            itemCode = item.ItemCode;
            itemName = item.itemName;
            itemCnt = item.itemCnt;
            itemImage.sprite = item.itemImage;
            itemInformation = item.itemInfo;

            if (item.ItemAmount == 1) isCntView = false;
            else isCntView = true;

            RefreshSlot();
        }
        else
        {
            RemoveSlot();
        }
    }

    public void RemoveSlot()
    {
        currentItem = null;
        itemType = ITEMTYPE.All;
        itemCode = "";
        itemName = "";
        itemCnt = 0;
        itemImage.sprite = null;
        itemInformation = "";
        RefreshSlot();
    }
     
    public void ViewAndHideInvenSlot(bool tf)
    {
        Color color = itemImage.GetComponent<Image>().color;

        // tf == true, invenslot has item
        if (tf)
        {
            // 불투명
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

        if (!isCntView)
            itemCntText.gameObject.SetActive(false);

        itemImage.GetComponent<Image>().color = color;
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
