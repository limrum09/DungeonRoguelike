using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShortKeyItem : MonoBehaviour
{
    [SerializeField]
    private ShortKeyManager shortKeyManager;
    [SerializeField]
    private InvenItem item;
    private int itemIndex;

    public int shortkeyIndex;

    public string inputShortKey;
    public GameObject viewPort;
    public Image ItemIcon;
    public Text itemCnt;
    public TextMeshProUGUI shortkeyNumber;

    private bool isInputKey;
    // Start is called before the first frame update
    void Start()
    {
        isInputKey = false;
        SetShortkeyNumber();

        // 저장된 것이 있는지 확인 필요

    }

    // Update is called once per frame
    void Update()
    {
        if(item != null)
        {
            if (Input.GetKeyDown(inputShortKey))
            {
                UseItem();
            }

            itemCnt.text = item.itemCnt.ToString();
        }        
    }

    public void SetItemIndex(int index)
    {
        itemIndex = index;
    }

    public void RegisterItem(int index)
    {
        int _index = index;

        item = InvenData.instance.invenSlots[_index];

        if(item != null)
        {
            ViewItem();

            shortKeyManager.InputItemInShortkey(shortkeyIndex);
        }        
    }

    private void ViewItem()
    {
        viewPort.SetActive(true);
        ItemIcon.sprite = item.itemImage;
        itemCnt.text = item.itemCnt.ToString();
    }

    public void HideItem()
    {
        item = null;
        ItemIcon.sprite = null;
        itemCnt.text = "";
        viewPort.SetActive(false);
    }

    private void UseItem()
    {
        InvenData.instance.UsingItem(item, itemIndex);

        itemCnt.text = item.itemCnt.ToString();
    }

    private void SetShortkeyNumber()
    {
        char text = inputShortKey[0];
        
        int s = Convert.ToInt32(text);

        if(97 <= s && s <= 122)
        {
            s -= 32;
        }

        text = Convert.ToChar(s);

        shortkeyNumber.text = text.ToString();
    }

    public InvenItem GetItem()
    {
        return item;
    }
}
