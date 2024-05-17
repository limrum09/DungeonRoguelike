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

    // �κ��丮 ���� ����
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

    // ������ ȹ��
    public void AddItem(InvenItem item)
    {
        bool addItemCount = false;
        // �κ��丮�� ���� �������� �����ϰ� �ִ��� Ȯ��
        for(int i = 0; i< invenSlots.Count; i++)
        {
            if(invenSlots[i] != null)
            {
                // ���� �������� �ִ� ���
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

        // ������ addItemCount = true�� ����
        if(addItemCount) return;
        // ���� ��� ����ִ� invenSlots�� ���Ӱ� �߰�
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

    // ���ο� ������ ȹ�� �� �κ��丮�� ������ �߰�
    private void AddItemInvenSlot(int _index)
    {
        int index = _index;

        InvenSlot invenSlot = invenContent.transform.GetChild(index).GetComponent<InvenSlot>();

        // itemName�� ����ִ��� Ȯ��
        if (string.IsNullOrEmpty(invenSlot.itemName))
        {
            invenSlot.SetSlotItme(invenSlots[index]);

            // �κ��丮�� ���� �� �Ͻ� ����
            if (invenButton.isSorting)
            {
                AddItemSort(invenSlots[index]);
            }            
        }
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
