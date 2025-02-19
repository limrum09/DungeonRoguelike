using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUIController : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private PlayerSaleItem saleItemPrefab;

    [Header("Transform")]
    [SerializeField]
    private Transform playerSaleItemTf;
    [SerializeField]
    private RectTransform playerSaleItemRectTf;

    [Header("Panel")]
    [SerializeField]
    private GameObject storeUI;
    [SerializeField]
    private PurchasePanel purchasePanel;
    [SerializeField]
    private SalePanel salePanel;

    private List<PlayerSaleItem> items = new List<PlayerSaleItem>();

    public void StoreUIStart()
    {
        for(int i = 0; i < 100; i++)
        {
            PlayerSaleItem newItem = Instantiate(saleItemPrefab, playerSaleItemTf);
            items.Add(newItem);
            items[i].gameObject.SetActive(false);
        }

        purchasePanel.gameObject.SetActive(false);
        salePanel.gameObject.SetActive(false);
        storeUI.SetActive(false);
    }

    public void SetStoreState(bool state)
    {
        storeUI.SetActive(state);

        if(state)
            SetStoreUI();
    }

    public void SetStoreUI()
    {
        var inven = InvenData.instance.invenSlots;
        int j = 0;
        for(int i = 0; i < inven.Count; i++)
        {
            if(inven[i] != null)
            {
                items[j].SettingSaleItem(inven[i], i);
                items[j].gameObject.SetActive(true);
                j++;
            }
        }
        for(int i = j; i < 100; i++)
        {
            items[i].gameObject.SetActive(false);
        }

        Vector2 tempSize = playerSaleItemRectTf.sizeDelta;
        tempSize.y = j * 70f;
        playerSaleItemRectTf.sizeDelta = tempSize;
    }

    public void OpenItemPurchasePanel(InvenItem item) => purchasePanel.OpenPurchasePanel(item);
    public void OpenItemSalePanel(InvenItem item, int saleItemIndex) => salePanel.OpenItemSalePanel(item, saleItemIndex);
}
