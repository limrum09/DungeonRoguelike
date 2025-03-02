using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InvenData : MonoBehaviour
{
    public static InvenData instance;
    public List<InvenItem> invenSlots = new List<InvenItem>();

    [SerializeField]
    private InventoryController invenController;
    [SerializeField]
    private GameObject invenContent;
    [SerializeField]
    private GameObject invenSlotPrefab;
    [SerializeField]
    private int goldCoinCount;


    private int invenCount;
    public int InvenGoldCoinCount => goldCoinCount;

    public void InvenDataStart()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        string path = Path.Combine(Application.persistentDataPath, "SaveFile");

        invenController = Manager.Instance.UIAndScene.InventoryUI;
        invenContent = invenController.Content;
        goldCoinCount = 0;

        // 처음 시작
        if (!File.Exists(path))
        {
            Debug.Log("First");
            Initialized();
            invenCount = invenSlots.Count;
        }
    }

    // SaveData가 있을 시, SaveDatabase.cs에서 호출
    public void Initialzed(int inventoryCount)
    {
        invenCount = inventoryCount;

        Initialized();
    }

    public void Initialized()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        int contentSlotCount = invenContent.transform.childCount;
        if (contentSlotCount > invenCount)
        {
            for (int i = invenCount; i < contentSlotCount; i++)
            {
                invenSlots.Add(null);
            }
            invenCount = contentSlotCount;
        }
        else if (contentSlotCount < invenCount)
        {
            for (int i = contentSlotCount; i < invenCount; i++)
            {
                InstantiateInvenSlot();
            }
        }

        if(invenCount > invenSlots.Count)
        {
            for(int i = invenSlots.Count; i < invenCount; i++)
            {
                invenSlots.Add(null);
            }
        }
        else if(invenCount < invenSlots.Count)
        {
            for(int i = invenCount; i < invenSlots.Count; i++)
            {
                invenSlots.RemoveAt(i);
            }
        }

        CallInvenSlot(invenCount);
        for (int i = 0; i < invenCount; i++)
        {
            GetInvenSlotComponent(i).SetSlotIndex(i);
        }
    }

    public int EmptyInvenCount()
    {
        int count = 0;

        foreach(var s in invenSlots)
        {
            if (s == null)
                count++;
        }

        return count;
    }

    // 인벤토리 개수 증가
    public void AddInventorySlotCount()
    {
        if (invenCount >= 100)
            return;

        for (int i = 0; i < 4; i++)
        {
            invenSlots.Add(null);

            // Add InvenSlotPrefab for inventory UI
            GameObject newInvenSlot = InstantiateInvenSlot();
            newInvenSlot.name = $"InvenSlot ({invenCount})";
            newInvenSlot.GetComponent<InvenSlot>().index = invenCount;

            invenCount++;
        }
    }

    // 골드를 더하거나 뺀다.
    public bool ChangeGlodValue(int value)
    {
        bool result = true;
        int tempGold = goldCoinCount + value;

        // value를 받은 이후, 현제 보유하고 있는 골드의 값이 0보다 작으면 값을 변경하지 않고 false를 return한다.
        if(tempGold < 0)
            result = false;
        else
            goldCoinCount += value;

        // 인벤토리나 게임화면에서 골드값이 변화는 것을 보여줘야함

        return result;
    }

    // 아이템 획득
    public void AddItem(InvenItem item, int getItemCnt = 1)
    {
        InvenItem newItem = item.Clone();

        // 인벤토리에 같은 아이템을 소유하고 있는지 확인
        for(int i = 0; i< invenSlots.Count; i++)
        {
            if(invenSlots[i] != null)
            {
                // 같은 아이템이 있는 경우
                if(invenSlots[i].ItemCode == newItem.ItemCode && !invenSlots[i].IsMax())
                {
                    invenSlots[i].AddItemCount(getItemCnt);
                    RefreshInvenSlot(i);
                    Manager.Instance.UIAndScene.ShortCutBox.CheckUsingShortKeyItem(invenSlots[i].ItemCode);
                    return;
                }
            }
        }

        // 같은 아이템이 없는 경우 비어있는 invenSlots에 새롭게 추가
        // 비어 있는 칸 찾기
        int nullSlotIndex = invenSlots.FindIndex(IsNULL);
        // 추가
        if (nullSlotIndex != -1)
        {
            invenSlots[nullSlotIndex] = newItem;
            invenSlots[nullSlotIndex].AddItemCount(getItemCnt);
            RefreshInvenSlot(nullSlotIndex);
            Manager.Instance.UIAndScene.ShortCutBox.CheckUsingShortKeyItem(invenSlots[nullSlotIndex].ItemCode);

            // 인벤토리가 정렬 중 일시 정렬
            if (invenController.isSorting)
            {
                ItemSort(invenSlots[nullSlotIndex]);
            }
        }
        // 비어 있는 칸이 없을 경우
        else
            Debug.Log("No null in List");
    }

    public void UsingInvenItem(InvenItem item)
    {
        string itemName = item.ItemName;
        for (int i = 0; i < invenSlots.Count; i++)
        {
            if (invenSlots[i] != null)
            {
                if (invenSlots[i].ItemName == itemName)
                {
                    ConsumeInvenItem(i);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// count에 특변한 값이 들어가지 않으면, player가 아이템을 사용한 것으로 간주
    /// count의 값이 0보다 큰 경우, 아이템을 여러개 버리거나 판매한다고 간주
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    public void ConsumeInvenItem(int index, int count = -1)
    {
        if (IsValidIndex(index))
        {
            InvenSlot invenSlot = GetInvenSlotComponent(index);

            if (invenSlot.CurrentItem != null)
            {
                if (invenSlots[index].ItemCnt == 0)
                    return;

                if (count == -1)
                {
                    invenSlots[index].AddItemCount(-1);
                    // 아이템 사용
                    invenSlots[index].UsingItem();
                }
                else if (count >= 0)
                {
                    invenSlots[index].AddItemCount(-count);
                }

                // 단축키에 아이템 있을 시, 개수 감소
                Manager.Instance.UIAndScene.ShortCutBox.CheckUsingShortKeyItem(invenSlot.ItemCode);

                if (invenSlots[index].ItemCnt <= 0)
                {
                    invenSlots[index] = null;
                }

                RefreshInvenSlot(index);
            }
        }
    }   

    public void RefreshInvenSlot(int index)
    {
        InvenSlot invenSlot = GetInvenSlotComponent(index);

        if(invenSlot != null)
        {
            invenSlot.SetSlotItem(invenSlots[index]);
        }
    }

    // currentIndex가 도착 InvenSlot, lastIndex가 아이템을 잡기 시작한 지점
    public void MoveInvenItem(int lastIndex, int currentIndex)
    {
        if (IsValidIndex(lastIndex) && IsValidIndex(currentIndex))
        {
            // Potion이고, itemCode가 동일할 경우, 포션을 합친다.
            if (invenSlots[currentIndex] != null && lastIndex != currentIndex) 
            {
                if(invenSlots[currentIndex].itemtype == ITEMTYPE.POTION && invenSlots[lastIndex].ItemCode == invenSlots[currentIndex].ItemCode)
                    CombinePotionCount(lastIndex, currentIndex);
                else
                    ChangeIndexInvenItem(lastIndex, currentIndex);
            }
            else
                ChangeIndexInvenItem(lastIndex, currentIndex);

            // 이전 위치의 슬롯과 이후 드레그를 마친 슬롯의 값을 다시 확인
            RefreshInvenSlot(lastIndex);
            RefreshInvenSlot(currentIndex);
        }
    }

    // 인벤슬롯 불러오기
    private void CallInvenSlot(int length)
    {
        for (int i = 0; i < length; i++)
        {
            InvenSlot invenSlot = GetInvenSlotComponent(i);

            if (invenSlots[i] == null)
            {
                invenSlot.RemoveSlot();
            }
            else if (invenSlots[i] != null && invenSlots[i].ItemCnt != 0)
            {
                invenSlot.SetSlotItem(invenSlots[i]);
                invenSlot.ViewAndHideInvenSlot(true);
            }
        }
    }

    // invenContent.transform.GetChild(index).GetComponent<InvenSlot>();
    private InvenSlot GetInvenSlotComponent(int index)
    {
        Transform selectInven = null;

        if (IsValidIndex(index))
        {
            selectInven = invenContent.transform.GetChild(index);
        }

        return selectInven != null ? selectInven.GetComponent<InvenSlot>() : null;
    }

    // index 값이 inventory의 개수르 넘기지 않는지 확인
    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < invenContent.transform.childCount;
    }

    private GameObject InstantiateInvenSlot()
    {
        GameObject newInvenSlot = Instantiate(invenSlotPrefab, this.transform.position, Quaternion.identity);
        newInvenSlot.transform.SetParent(invenContent.transform, false);

        return newInvenSlot;
    }

    private bool IsNULL(InvenItem slot)
    {
        return slot == null;
    }

    // 종류별로 정렬
    private void ItemSort(InvenItem item)
    {

        if (item.itemtype == ITEMTYPE.POTION)
        {
            invenController.PostionSortButton();
        }
        else if (item.itemtype == ITEMTYPE.ETC)
        {
            invenController.ETCSortButton();
        }
    }

    // 드레그 하여 같은 포션을 겹친다면 아이템 합치기
    private void CombinePotionCount(int lastIndex, int currentIndex)
    {
        int ItemAmount = invenSlots[currentIndex].ItemAmount;
        int currentIndexItemCount = invenSlots[currentIndex].ItemCnt;

        // 드레그한 위치의 포션의 개수가 Amount보다 적을 시, 동작
        if (currentIndexItemCount < ItemAmount)
        {
            int lastIndexItemCount = invenSlots[lastIndex].ItemCnt;
            int itemSum = lastIndexItemCount + currentIndexItemCount;

            // 현제 두 포션의 개수의 합이 Amount보다 많을 시, true. 예) Amount가 99라면 합이 최소 100은 어야 true.
            if(itemSum > ItemAmount)
            {
                lastIndexItemCount = itemSum - ItemAmount;
                currentIndexItemCount = ItemAmount;

                invenSlots[lastIndex].SetItemCount(lastIndexItemCount);
            }
            else
            {
                currentIndexItemCount = itemSum;

                invenSlots[lastIndex] = null;
            }

            invenSlots[currentIndex].SetItemCount(currentIndexItemCount);
        }
    }

    private void ChangeIndexInvenItem(int lastIndex, int currentIndex)
    {
        InvenItem tempInvenItem = invenSlots[currentIndex];
        invenSlots[currentIndex] = invenSlots[lastIndex];
        invenSlots[lastIndex] = tempInvenItem;
    }
}
