using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPurchaseItem : MonoBehaviour
{
    [SerializeField]
    private InvenItem item;             // 판매하는 아이템

    [SerializeField]
    private Image itemIcon;             // 아이콘
    [SerializeField]
    private TextMeshProUGUI itemName;   // 이름
    [SerializeField]
    private TextMeshProUGUI itemPrice;  // 가격

    private float clickTime;
    // Start is called before the first frame update
    void Awake()
    {
        if(item != null)
        {
            itemIcon.sprite = item.ItemImage;
            itemName.text = item.ItemName;
            itemPrice.text = item.ItemPrice.ToString();
            clickTime = -1f;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void ItemImageClick()
    {
        // 이전에 클릭한 시간보다 현제 시간과의 차이가 0.25f보다 작다면 더블클릭을 한 것으로 간주
        if ((Time.time - clickTime) < 0.25f)
        {
            Manager.Instance.UIAndScene.StoreUI.OpenItemPurchasePanel(item);
        }
        else
        {
            // 더블클릭이 아니면 현제 시간으로 덮어 씌운다
            clickTime = Time.time;
        }
    }
}
