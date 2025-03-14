using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItemDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler, IEndDragHandler
{
    private bool isDrag;

    [SerializeField]
    private GameObject itemIcon;
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private Image iconImage;

    int dragStartIndex;
    int dragEndIndex;

    private float clickTimer;
    private bool isDoubleClick;

    InvenSlot clickInven;

    private int FindIndex(InvenSlot indexUI)
    {
        return indexUI.index;
    }

    // Click InvenSlot
    public void OnPointerDown(PointerEventData eventData)
    {
        // When player clicked InvenSlot, save the InvenSlot in the mouse pointer position to the GameObject
        var clickUI = eventData.pointerEnter;

        Debug.Log("Pointer Down Item : " + clickUI.gameObject.name);

        // if clickUI has InvenSlot componenet, clickInven has clickUI's InvenSlot
        if (clickUI != null && clickUI.GetComponent<InvenSlot>())
        {
            clickInven = clickUI.GetComponent<InvenSlot>();
            Debug.Log("Click Inven : " + clickInven.name);
        }
        else
        {
            Debug.Log("Don't have InvenSlot.cs");
            clickInven = null;
        }

        // if clickInven include 'InvenSlot' component, player can use and drag item
        if (clickInven != null && clickInven.CurrentItem != null)
        {
            dragStartIndex = FindIndex(clickInven);

            DoubleClick();
            if (isDoubleClick && clickInven.ItemType == ITEMTYPE.POTION)
            {
                InvenData.instance.ConsumeInvenItem(dragStartIndex);
            }

            Debug.Log("Current Item Count : " + clickInven.ItemCnt);
            icon.SetActive(true);
            itemIcon.transform.position = Input.mousePosition;
            iconImage.sprite = clickInven.ItemImage.sprite;
            
            
            isDrag = true;
        }
        else if(clickInven == null)
        {
            Debug.Log("InvenSlot doesn't have InvenSlot.cs");
        }
        else
        {
            Debug.Log("Error");
        }
    }

    // Drag Start
    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag == true)
        {
            itemIcon.transform.position = Input.mousePosition;
        }
    }

    // Drag End
    public void OnEndDrag(PointerEventData eventData)
    {
        
        var dragEndUI = eventData.pointerEnter;

        // Item move to dragEndUI's position
        InvenSlot dragEndInven;
        if (dragEndUI.GetComponent<InvenSlot>())
        {
            dragEndInven = dragEndUI.GetComponent<InvenSlot>();
        }
        else
        {
            dragEndInven = null;
        }

        if(dragEndInven != null)
        {
            dragEndIndex = FindIndex(dragEndInven);

            InvenData.instance.MoveInvenItem(dragStartIndex, dragEndIndex);
        }

        // input item for shortkey location
        ShortKeyItem shortKey;
        if (dragEndUI.GetComponent<ShortKeyItem>() && clickInven.ItemType == ITEMTYPE.POTION)
        {
            shortKey = dragEndUI.GetComponent<ShortKeyItem>();
        }
        else
        {
            shortKey = null;
        }

        if(shortKey != null)
        {
            shortKey.RegisterInput(dragStartIndex);
        }
    }

    // Click End
    public void OnPointerUp(PointerEventData eventData)
    {
        icon.SetActive(false);
        isDrag = false;
    }

    // Check Double Click, player used Item
    private void DoubleClick()
    {
        if((Time.time - clickTimer) <= 0.25f)
        {
            isDoubleClick = true;
            clickTimer = -1.0f;
        }
        else
        {
            isDoubleClick = false;
            clickTimer = Time.time;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        clickTimer = -1.0f;
        isDoubleClick = false;
        isDrag = false;
    }
}
