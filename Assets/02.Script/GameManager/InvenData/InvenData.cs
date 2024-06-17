using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
     
    public InventoryButton InvenButton => invenButton;
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
    }

    private void Start()
    {
        /*if (!LoadFile("invenFile"))
        {
            Initialized(invenButton, invenContent);
        }*/

        string path = Path.Combine(Application.persistentDataPath, "SaveFile");
        
        // 처음 시작
        if(!File.Exists(path))
        {
            Debug.Log("First");
            Initialized(InvenButton, invenContent);
            invenCount = invenSlots.Count;
        }
    }

    private void OnApplicationQuit()
    {
        // SaveInvenDataToFile("invenFile");
    }

    // SaveData가 있을 시, SaveDatabase.cs에서 호출
    public void Initialzed(int inventoryCount)
    {
        invenCount = inventoryCount;

        Initialized(invenButton, invenContent);
    }

    public void Initialized(InventoryButton button, GameObject content)
    {
        invenContent = content;
        invenButton = button;

        InitializeInventory();
    }

    private void InitializeInventory()
    {
        

        int contentSlotCount = invenContent.transform.childCount;
        if (contentSlotCount > invenCount)
        {
            for (int i = invenCount; i < contentSlotCount; i++)
            {
                // instance
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

    private GameObject InstantiateInvenSlot()
    {
        GameObject newInvenSlot = Instantiate(invenSlotPrefab, this.transform.position, Quaternion.identity);
        newInvenSlot.transform.SetParent(invenContent.transform, false);

        return newInvenSlot;
    }

    // 인벤토리 개수 증가
    public void AddInventorySlotCount()
    {
        for (int i = 0; i < 6; i++)
        {
            invenSlots.Add(null);

            // Add InvenSlotPrefab for inventory UI
            GameObject newInvenSlot = InstantiateInvenSlot();
            newInvenSlot.name = $"InvenSlot ({invenCount})";
            newInvenSlot.GetComponent<InvenSlot>().index = invenCount;

            invenCount++;
        }
    }

    // 아이템 획득
    public void CheckItem(InvenItem item)
    {
        InvenItem newItem = item.Clone();

        // 인벤토리에 같은 아이템을 소유하고 있는지 확인
        for(int i = 0; i< invenSlots.Count; i++)
        {
            if(invenSlots[i] != null)
            {
                // 같은 아이템이 있는 경우
                if(invenSlots[i].ItemCode == newItem.ItemCode)
                {
                    if (!invenSlots[i].IsMax())
                    {
                        invenSlots[i].itemCnt++;
                        RefreshInvenSlot(i);
                        return;
                    }
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
            invenSlots[nullSlotIndex].itemCnt = 1;
            RefreshInvenSlot(nullSlotIndex);

            // 인벤토리가 정렬 중 일시 정렬
            if (invenButton.isSorting)
            {
                AddItemSort(invenSlots[nullSlotIndex]);
            }
        }
        // 비어 있는 칸이 없을 경우
        else
            Debug.Log("No null in List");
    }

    private bool IsNULL(InvenItem slot)
    {
        return slot == null;
    }

    // 종류별로 정렬
    private void AddItemSort(InvenItem item)
    {
        
        if(item.itemtype == ITEMTYPE.POTION)
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
        for (int i = 0; i < length; i++)
        {
            InvenSlot invenSlot = GetInvenSlotComponent(i);

            if (invenSlots[i] == null)
            {
                invenSlot.RemoveSlot();
            }
            else if(invenSlots[i] != null && invenSlots[i].itemCnt != 0)
            {
                invenSlot.SetSlotItem(invenSlots[i]);
                invenSlot.ViewAndHideInvenSlot(true);
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

    private void RemoveInvenSlot(int index)
    {
        InvenSlot invenSlot = GetInvenSlotComponent(index);

        if(invenSlot != null)
        {
            invenSlot.RemoveSlot();
        }
    }

    public void MoveInvenItem(int lastIndex, int currentIndex)
    {
        if(IsValidIndex(lastIndex) && IsValidIndex(currentIndex))
        {
            InvenItem tempInvenItem = invenSlots[currentIndex];
            invenSlots[currentIndex] = invenSlots[lastIndex];
            invenSlots[lastIndex] = tempInvenItem;

            RemoveInvenSlot(lastIndex);

            RefreshInvenSlot(lastIndex);
            RefreshInvenSlot(currentIndex);
        }
    }

    public void UsingInvenItem(int index)
    {
        if (IsValidIndex(index))
        {
            InvenSlot invenSlot = GetInvenSlotComponent(index);

            if (invenSlot != null)
            {
                invenSlots[index].itemCnt--;

                if (invenSlots[index].itemCnt == 0)
                {
                    invenSlot.RemoveSlot();
                    invenSlots[index] = null;
                }
                else if(invenSlots[index].itemCnt < 0)
                {
                    invenSlots[index].itemCnt = 0;
                }

                RefreshInvenSlot(index);
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

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < invenContent.transform.childCount;
    }
}
