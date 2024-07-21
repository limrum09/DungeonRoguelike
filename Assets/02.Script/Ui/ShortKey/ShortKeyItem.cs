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

    [Header("Public")]
    public TextMeshProUGUI shortkeyNumber;
    public string inputShortKey;
    private int shortkeyIndex;

    [Header("Item")]
    [SerializeField]
    private InvenItem item;
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private GameObject viewPort;
    [SerializeField]
    private Text itemCnt;
    private int itemIndex;

    [Header("Skill")]
    [SerializeField]
    private ActiveSkill skill;
    [SerializeField]
    private Image skillImgeSlot;

    // Start is called before the first frame update
    void Start()
    {
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
                if (item != null)
                {
                    itemCnt.text = ItemCount().ToString();
                    UseItem();
                }                    
                else if (skill != null)
                    UseSkill();
            }
        }
    }

    public void SetIndex(int index)
    {
        shortkeyIndex = index;

        // 저장된 단축키가 없을 경우
        HideItem();
        HideSkill();
    }

    public void SetItemIndex(int index)
    {
        itemIndex = index;
    }

    public void RegisterSkill(ActiveSkill getSkill)
    {
        ActiveSkill newSkill = getSkill.SkillClone();

        skill = newSkill;

        HideItem();

        if(skill != null)
        {
            Viewskill(skill);

            shortKeyManager.InputSkillInShortkey(shortkeyIndex);
        }
    }

    public void RegisterItem(int index)
    {
        itemIndex = index;

        item = InvenData.instance.invenSlots[itemIndex];

        HideSkill();

        if(item != null)
        {
            ViewItem();

            // 다른 단축키에서 해당 아이템 제거
            shortKeyManager.InputItemInShortkey(shortkeyIndex);
        }        
    }

    public void HideSkill()
    {
        skill = null;
        skillImgeSlot.sprite = null;
        skillImgeSlot.gameObject.SetActive(false);
    }

    public void HideItem()
    {
        item = null;
        itemIcon.sprite = null;
        itemCnt.text = "";
        viewPort.SetActive(false);
    }

    private void Viewskill(ActiveSkill activeSkill)
    {
        skillImgeSlot.gameObject.SetActive(true);
        skillImgeSlot.sprite = activeSkill.icon;
    }

    private void ViewItem()
    {
        viewPort.SetActive(true);
        itemIcon.sprite = item.itemImage;
        itemCnt.text = ItemCount().ToString();
    }

    private void UseSkill()
    {
        
    }

    private void UseItem()
    {
        InvenData.instance.UsingInvenItem(item);

        itemCnt.text = ItemCount().ToString();
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

    private int ItemCount()
    {
        int count = 0;

        foreach(var inven in InvenData.instance.invenSlots)
        {
            if(inven != null && item != null)
            {
                if (inven.itemName == item.itemName)
                    count += inven.itemCnt;
            }
        }

        return count;
    }

    public InvenItem GetItem()
    {
        return item;
    }

    public ActiveSkill GetSkill()
    {
        return skill;
    }
}
