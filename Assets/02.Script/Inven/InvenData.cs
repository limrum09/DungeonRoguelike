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

        if(!File.Exists(path))
        {
            Debug.Log("First");

            // No Save Inven Data, If you have save data, it need to be modify.
            invenCount = invenSlots.Count;
        }
        else
        {
            invenCount = SaveDatabase.instance.invenSaveDatas.inventoryCount;
            Debug.Log("Inven Count : " + invenCount);
        }

        Initialized(invenButton, invenContent);
    }

    private void OnApplicationQuit()
    {
        // SaveInvenDataToFile("invenFile");
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
    public void CheckItem(InvenItem item)
    {
        InvenItem newItem = item.Clone();

        // �κ��丮�� ���� �������� �����ϰ� �ִ��� Ȯ��
        for(int i = 0; i< invenSlots.Count; i++)
        {
            if(invenSlots[i] != null)
            {
                // ���� �������� �ִ� ���
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

        // ���� �������� ���� ��� ����ִ� invenSlots�� ���Ӱ� �߰�
        // ��� �ִ� ĭ ã��
        int nullSlotIndex = invenSlots.FindIndex(IsNULL);
        // �߰�
        if (nullSlotIndex != -1)
        {
            invenSlots[nullSlotIndex] = newItem;
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
