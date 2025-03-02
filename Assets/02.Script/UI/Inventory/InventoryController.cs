using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject invenContent;

    public GameObject Content => invenContent;
    public bool isSorting;
    // Start is called before the first frame update
    public void InvenToryStart()
    {
        isSorting = false;
    }


    public void PostionSortButton()
    {
        foreach (Transform child in invenContent.transform)
        {
            InvenSlot childInvenSlot = child.GetComponent<InvenSlot>();
            if (childInvenSlot.ItemType == ITEMTYPE.POTION)
            {
                childInvenSlot.gameObject.SetActive(true);
            }
            else
            {
                childInvenSlot.gameObject.SetActive(false);
            }
        }

        isSorting = true;
    }

    public void ETCSortButton()
    {
        foreach (Transform child in invenContent.transform)
        {
            InvenSlot childInvenSlot = child.GetComponent<InvenSlot>();
            if (childInvenSlot.ItemType == ITEMTYPE.ETC)
            {
                childInvenSlot.gameObject.SetActive(true);
            }
            else
            {
                childInvenSlot.gameObject.SetActive(false);
            }
        }

        isSorting = true;
    }

    public void AllButton()
    {
        foreach (Transform child in invenContent.transform)
        {
            InvenSlot childInvenSlot = child.GetComponent<InvenSlot>();
            childInvenSlot.gameObject.SetActive(true);
        }

        isSorting = false;
    }

    public void AddInventoryCount()
    {
        Manager.Instance.Game.InvenDatas.AddInventorySlotCount();
    }
}
