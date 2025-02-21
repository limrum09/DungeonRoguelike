using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreUIController : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private PlayerSaleItem saleItemPrefab;      // 플레이어가 판매하는 아이템 정보를 보여주는 프리펩

    [Header("Transform")]
    [SerializeField]
    private Transform playerSaleItemTf;         // saleItemPrefab을 삽입 하는 위치
    [SerializeField]
    private RectTransform playerSaleItemRectTf; // saleItemPrefab의 개수에 따라 값이 변하는 스크롤의 컴포넌트

    [Header("Panel")]
    [SerializeField]
    private GameObject storeUI;                 // 상점 UI
    [SerializeField]
    private PurchasePanel purchasePanel;        // 아이템 구매 UI
    [SerializeField]
    private SalePanel salePanel;                // 아이템 판매 UI

    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI goldCoinValaueText; // 골드 값 UI

    private List<PlayerSaleItem> items = new List<PlayerSaleItem>();    // saleItemPrefab의 재사용을 위한 List

    public void StoreUIStart()
    {
        // 게임 시작시, 비어있는 프리팹을 플레이어 인벤토리 개수만큼 만든다.
        // 플레이어 판매 창(InvenItemViewFrame)에 들어가는 프리펩으로 플레이어가 현재 판매가능한 아이템들을 보여준다.
        for(int i = 0; i < 100; i++)
        {
            PlayerSaleItem newItem = Instantiate(saleItemPrefab, playerSaleItemTf);
            items.Add(newItem);
            items[i].gameObject.SetActive(false);
        }

        // 안보이도록 처음 초기화
        purchasePanel.gameObject.SetActive(false);
        salePanel.gameObject.SetActive(false);
        storeUI.SetActive(false);
    }

    // Store UI는 플레이어가 Store NPC와 상호작용하면 바로 켜지기나 꺼져야 한다.
    // 플레이어가 NPC의 상호작용 범위 안에서 상호작용을 시도하는 경우
    public void SetStoreState()
    {
        bool state = !storeUI.activeSelf;

        storeUI.SetActive(state);

        if (state)
        {
            SetStoreUI();
            RefreshGoldCoinValue();
        }
        // UI가 꺼지는 경우 단축키 사용 가능
        else
        {
            Manager.Instance.canInputKey = true;
        }
    }

    // 플레이어가 NPC를 떠나가거나, 다른 외부요인으로 Store를 사용하는 경우
    public void SetStoreState(bool state)
    {
        storeUI.SetActive(state);
    }

    public void RefreshGoldCoinValue()
    {
        goldCoinValaueText.text = InvenData.instance.InvenGoldCoinCount.ToString();
    }

    public void SetStoreUI()
    {
        var inven = InvenData.instance.invenSlots;
        int j = 0;  // 현제 사용 중인(아이템이 있는) 인벤토리 개수

        // 현제 인벤토리 개수만큼 실행
        for (int i = 0; i < inven.Count; i++)
        {
            // 인벤토리가 비어 있지 않은 경우
            if(inven[i] != null)
            {
                // 오브젝트에 인벤토리의 정보를 보내주고 활성화
                items[j].SettingSaleItem(inven[i], i);
                items[j].gameObject.SetActive(true);
                j++;
            }
        }

        // 인벤토리에서 사용중인 아이템 칸을 초과하는 게임 오브젝트들 비활성화
        for(int i = j; i < 100; i++)
        {
            items[i].gameObject.SetActive(false);
        }

        // 활성화 중인 프리펩의 개수에 맞추어 크기 재설정
        Vector2 tempSize = playerSaleItemRectTf.sizeDelta;
        tempSize.y = j * 70f;
        playerSaleItemRectTf.sizeDelta = tempSize;
    }

    public void OpenItemPurchasePanel(InvenItem item) => purchasePanel.OpenPurchasePanel(item);
    public void OpenItemSalePanel(InvenItem item, int saleItemIndex) => salePanel.OpenItemSalePanel(item, saleItemIndex);
}
