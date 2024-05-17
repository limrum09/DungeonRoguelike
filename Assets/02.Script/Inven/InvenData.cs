using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenData : MonoBehaviour
{
    public static InvenData instance;

    private int invenCount;

    public List<InvenItem> invenSlots = new List<InvenItem>();
    [SerializeField]
    private InventoryButton invenButton;
    [SerializeField]
    private GameObject invenContent;
    [SerializeField]
    private GameObject invenSlotPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // No Save Inven Data, If you have save data, it need to be modify.
        invenCount = invenSlots.Count;
        
        int contentSlotCount = invenContent.transform.childCount;
        if (contentSlotCount > invenCount)
        {
            for(int i = invenCount; i< contentSlotCount; i++)
            {
                // instance
                InvenItem newInven = ScriptableObject.CreateInstance<InvenItem>();
                invenSlots.Add(newInven);
                invenSlots[i] = null;
            }
            invenCount = contentSlotCount;
        }
        else if(contentSlotCount == invenCount)
        {
            invenCount = contentSlotCount;
        }
        else if(contentSlotCount < invenCount)
        {
            for (int i = contentSlotCount; i < invenCount; i++)
            {
                GameObject newInvenSlot = Instantiate(invenSlotPrefab, this.transform.position, Quaternion.identity);
                newInvenSlot.transform.SetParent(invenContent.transform, false);
            }
        }

        CallInvenSlot(invenCount);
        for (int i = 0; i < invenCount; i++)
        {
            invenContent.transform.GetChild(i).GetComponent<InvenSlot>().SetSlotIndex(i);
        }
    }

    // 인벤토리 개수 증가
    public void AddInventorySlotCount()
    {
        for (int i = 0; i < 6; i++)
        {
            InvenItem newInven = ScriptableObject.CreateInstance<InvenItem>();
            invenSlots.Add(newInven);
            invenSlots[invenCount] = null;
            
            // Add InvenSlotPrefab for inventory UI
            GameObject newInvenSlot = Instantiate(invenSlotPrefab, this.transform.position, Quaternion.identity);
            newInvenSlot.transform.SetParent(invenContent.transform, false);
            newInvenSlot.name = "InvenSlot (" + invenCount + ")";
            newInvenSlot.GetComponent<InvenSlot>().index = invenCount;

            invenCount++;
        }

    }

    public void RefreshTest()
    {
        CallInvenSlot(invenSlots.Count);
    }

    // 아이템 획득
    public void AddItem(InvenItem item)
    {
        bool addItemCount = false;
        // 인벤토리에 같은 아이템을 소유하고 있는지 확인
        for(int i = 0; i< invenSlots.Count; i++)
        {
            if(invenSlots[i] != null)
            {
                // 같은 아이템이 있는 경우
                if(invenSlots[i].itemName == item.itemName)
                {
                    addItemCount = true;
                    if (!item.IsMax())
                    {
                        invenSlots[i].itemCnt++;
                        invenContent.transform.GetChild(i).GetComponent<InvenSlot>().itemCnt = invenSlots[i].itemCnt;
                        invenContent.transform.GetChild(i).GetComponent<InvenSlot>().RefreshSlot();
                    } 
                }
            }
        }

        // 있으면 addItemCount = true로 종료
        if(addItemCount) return;
        // 없는 경우 비어있는 invenSlots에 새롭게 추가
        else
        {
            int nullSlotIndex = invenSlots.FindIndex(IsNULL);
            if (nullSlotIndex != -1)
            {
                invenSlots[nullSlotIndex] = item;
                invenSlots[nullSlotIndex].itemCnt = 1;          
                AddItemInvenSlot(nullSlotIndex);
            }
            else
                Debug.Log("No null in List");
        }
        
    }

    private bool IsNULL(InvenItem slot)
    {
        return slot == null;
    }

    // 새로운 아이템 획득 시 인벤토리에 아이템 추가
    private void AddItemInvenSlot(int _index)
    {
        int index = _index;

        InvenSlot invenSlot = invenContent.transform.GetChild(index).GetComponent<InvenSlot>();

        // itemName이 비어있는지 확인
        if (string.IsNullOrEmpty(invenSlot.itemName))
        {
            invenSlot.SetSlotItme(invenSlots[index]);

            // 인벤토리가 정렬 중 일시 정렬
            if (invenButton.isSorting)
            {
                AddItemSort(invenSlots[index]);
            }            
        }
    }

    // 종류별로 정렬
    private void AddItemSort(InvenItem item)
    {
        
        if(item.itemtype == ITEMTYPE.POSTION)
        {
            invenButton.PostionSortButton();
        }
        else if(item.itemtype == ITEMTYPE.ETC)
        {
            invenButton.ETCSortButton();
        }
    }

    private void CallInvenSlot(int length)
    {        
        int _length = length;
        for (int i = 0; i < _length; i++)
        {
            InvenSlot invenSlot = invenContent.transform.GetChild(i).GetComponent<InvenSlot>();

            if (invenSlots[i] != null && invenSlots[i].itemCnt != 0)
            {
                invenSlot.SetSlotItme(invenSlots[i]);
                invenSlot.ViewAndHideInvenSlot(true);
            }
            else
            {
                invenSlot.RemoveSlot();
            }
        }
    }

    private void RefreshInvenSlot(int _index)
    {
        int index = _index;

        InvenSlot invenSlot = invenContent.transform.GetChild(index).GetComponent<InvenSlot>();

        invenSlot.SetSlotItme(invenSlots[index]);
    }

    private void RemoveInvenSlot(int _index)
    {
        int index = _index;

        InvenSlot invenSlot = invenContent.transform.GetChild(index).GetComponent<InvenSlot>();

        invenSlot.RemoveSlot();
    }

    public void MoveInvenItem(int lastIndex, int currentIndex)
    {
        InvenItem tempInvenItem;
        tempInvenItem = invenSlots[currentIndex];

        invenSlots[currentIndex] = invenSlots[lastIndex];
        invenSlots[lastIndex] = tempInvenItem;

        RemoveInvenSlot(lastIndex);
        RefreshInvenSlot(lastIndex);
        RefreshInvenSlot(currentIndex);
    }

    public void UsingInvenItem(int index)
    {
        InvenSlot invenSlot = invenContent.transform.GetChild(index).GetComponent<InvenSlot>();

        int _index = index;
        invenSlots[_index].itemCnt--;
        RefreshInvenSlot(index);

        if (invenSlots[_index].itemCnt == 0)
        {
            invenSlot.RemoveSlot();
            invenSlots[_index] = null;
        }
    }

    public void UsingItem(InvenItem item, int index)
    {
        if (item.itemCnt >= 1)
        {
            item.itemCnt--;
        }
        else if (item.itemCnt <= 0) item.itemCnt = 0;

        RefreshInvenSlot(index);
    }
}
