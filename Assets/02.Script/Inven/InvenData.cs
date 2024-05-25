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
    }

    public void Initialized(InventoryButton button, GameObject content)
    {
        invenContent = content;
        invenButton = button;

        InitializeInventory();
    }

    private void InitializeInventory()
    {
        // No Save Inven Data, If you have save data, it need to be modify.
        invenCount = invenSlots.Count;

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
        else if (contentSlotCount == invenCount)
        {
            invenCount = contentSlotCount;
        }
        else if (contentSlotCount < invenCount)
        {
            for (int i = contentSlotCount; i < invenCount; i++)
            {
                InstantiateInvenSlot();
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

    // �κ��丮 ���� ����
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

    // ������ ȹ��
    public void AddItem(InvenItem item)
    {
        // �κ��丮�� ���� �������� �����ϰ� �ִ��� Ȯ��
        for(int i = 0; i< invenSlots.Count; i++)
        {
            if(invenSlots[i] != null)
            {
                // ���� �������� �ִ� ���
                if(invenSlots[i].itemName == item.itemName)
                {
                    if (!item.IsMax())
                    {
                        invenSlots[i].itemCnt++;
                        RefreshInvenSlot(i);
                        return;
                    } 
                }
            }
        }

        // ���� �������� ���� ��� ����ִ� invenSlots�� ���Ӱ� �߰�
        // ��� �ִ� ĭ ã��
        int nullSlotIndex = invenSlots.FindIndex(IsNULL);
        // �߰�
        if (nullSlotIndex != -1)
        {
            invenSlots[nullSlotIndex] = item;
            invenSlots[nullSlotIndex].itemCnt = 1;
            RefreshInvenSlot(nullSlotIndex);

            // �κ��丮�� ���� �� �Ͻ� ����
            if (invenButton.isSorting)
            {
                AddItemSort(invenSlots[nullSlotIndex]);
            }
        }
        // ��� �ִ� ĭ�� ���� ���
        else
            Debug.Log("No null in List");

    }

    private bool IsNULL(InvenItem slot)
    {
        return slot == null;
    }

    // �������� ����
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
        for (int i = 0; i < length; i++)
        {
            InvenSlot invenSlot = GetInvenSlotComponent(i);

            if (invenSlots[i] != null && invenSlots[i].itemCnt != 0)
            {
                invenSlot.SetSlotItem(invenSlots[i]);
                invenSlot.ViewAndHideInvenSlot(true);
            }
            else
            {
                invenSlot.RemoveSlot();
            }
        }
    }

    private void RefreshInvenSlot(int index)
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
                RefreshInvenSlot(index);

                if (invenSlots[index].itemCnt == 0)
                {
                    invenSlot.RemoveSlot();
                    invenSlots[index] = null;
                }
            }
        }
    }

    public void UsingItem(InvenItem item, int index)
    {
        if (!IsValidIndex(index)) return;

        if (item.itemCnt >= 1)
        {
            item.itemCnt--;
        }
        else if (item.itemCnt <= 0) item.itemCnt = 0;

        RefreshInvenSlot(index);
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
