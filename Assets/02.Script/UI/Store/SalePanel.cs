using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField itemCountField;

    [Header("Panel")]
    [SerializeField]
    private GameObject itemCountErrorPanel;

    private int itemCount;
    private int itemIndex = -1;
    private InvenItem item = null;

    public void OpenItemSalePanel(InvenItem saleItem, int saleItemIndex)
    {
        this.gameObject.SetActive(true);
        itemIndex = saleItemIndex;
        item = saleItem;
        itemCount = 1;
    }

    public void ItemSale()
    {
        InvenItem tempItem = InvenData.instance.invenSlots[itemIndex];
        if (tempItem == null)
            return;

        itemCount = int.Parse(itemCountField.text);

        if (tempItem.ItemCnt < itemCount)
        {
            itemCountErrorPanel.SetActive(true);
            return;
        }

        int itemPrice = item.ItemPrice * itemCount;

        InvenData.instance.ConsumeInvenItem(itemIndex, itemCount);
        InvenData.instance.ChangeGlodValue(itemPrice);
        Manager.Instance.UIAndScene.StoreUI.SetStoreUI();

        CloseSale();
    }

    public void CloseSale()
    {
        itemCountField.text = "1";
        itemCount = 1;
        item = null;

        this.gameObject.SetActive(false);
    }
}
