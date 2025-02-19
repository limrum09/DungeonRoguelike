using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSaleItem : MonoBehaviour
{
    [SerializeField]
    private InvenItem item;             // 현제 보유중인 아이템

    [SerializeField]
    private Image itemIcon;             // 아이콘
    [SerializeField]
    private TextMeshProUGUI itemName;   // 이름
    [SerializeField]
    private TextMeshProUGUI itemPrice;  // 가격
    [SerializeField]
    private TextMeshProUGUI ItemCount;  // 아이템 보유 개수

    private int saleItemIndex;
    private float clickTime;

    public void SettingSaleItem(InvenItem saleItem,  int index)
    {
        if(saleItem != null)
        {
            item = saleItem;
            saleItemIndex = index;

            itemIcon.sprite = item.ItemImage;
            itemName.text = item.ItemName;
            itemPrice.text = item.ItemPrice.ToString();
            ItemCount.text = item.ItemCnt.ToString();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ItemImageClick()
    {
        // 이전에 클릭한 시간보다 현제 시간과의 차이가 0.25f보다 작다면 더블클릭을 한 것으로 간주
        if ((Time.time - clickTime) < 0.25f)
        {
            Manager.Instance.UIAndScene.StoreUI.OpenItemSalePanel(item, saleItemIndex);
        }
        else
        {
            // 더블클릭이 아니면 현제 시간으로 덮어 씌운다
            clickTime = Time.time;
        }
    }
}
