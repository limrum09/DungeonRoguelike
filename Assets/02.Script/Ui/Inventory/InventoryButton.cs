using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private GameObject content;

    public GameObject Content => content;

    public bool isSorting;
    // Start is called before the first frame update
    void Start()
    {
        isSorting = false;
    }


    public void PostionSortButton()
    {
        foreach(Transform child in content.transform)
        {
            InvenSlot childInvenSlot = child.GetComponent<InvenSlot>();
            if (childInvenSlot.itemType == ITEMTYPE.POSTION)
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
        foreach (Transform child in content.transform)
        {
            InvenSlot childInvenSlot = child.GetComponent<InvenSlot>();
            if (childInvenSlot.itemType == ITEMTYPE.ETC)
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
        foreach (Transform child in content.transform)
        {
            InvenSlot childInvenSlot = child.GetComponent<InvenSlot>();
            childInvenSlot.gameObject.SetActive(true);
        }

        isSorting = false;
    }
}
