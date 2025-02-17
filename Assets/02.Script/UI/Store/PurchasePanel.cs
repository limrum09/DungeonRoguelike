using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField itemCountField;

    [Header("Panels")]
    [SerializeField]
    private GameObject scarceInvenSlotPanel;
    [SerializeField]
    private GameObject scarcehGoldPanel;

    private int itemCount;
    private InvenItem item = null;

    public void OpenPurchasePanel(InvenItem selectItem)
    {
        this.gameObject.SetActive(true);
        item = selectItem;
    }

    public void ItemPurchase()
    {
        itemCount = int.Parse(itemCountField.text);
        int emptyCount = InvenData.instance.EmptyInvenCount();

        if (item == null)
        {
            Debug.LogError("구매할 아이템의 정보가 없습니다.");
            return;
        }            

        if(itemCount > (emptyCount * item.ItemAmount))
        {
            scarceInvenSlotPanel.SetActive(true);
            return;
        }
        
        if(!InvenData.instance.ChangeGlodValue(itemCount * item.ItemPrice))
        {
            scarcehGoldPanel.SetActive(true);
            return;
        }

        InvenData.instance.AddItem(item, itemCount);
        ClosePurchase();
    }

    public void ClosePurchase()
    {
        itemCountField.text = "1";
        itemCount = 1;
        item = null;

        this.gameObject.SetActive(false);
    }
}
