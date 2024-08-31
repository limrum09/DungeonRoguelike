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
    private string inputShortKey;
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

    [Header("CoolTime")]
    [SerializeField]
    private Image coolTimeImage;

    public string InputShortKey => inputShortKey;

    private float coolTimer;
    public int ItemIndex
    {
        get
        {
            int index = itemIndex;
            if (item == null)
                index = -1;
            return index;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(item != null || skill != null)
        {
            if (Input.GetKeyDown(Manager.Instance.Key.GetKeyCode(inputShortKey)))
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

    public void SetIndex(int index, string key)
    {
        shortkeyIndex = index;

        SetShoryKey(key);

        // 저장된 단축키가 없을 경우
        if (skill == null)
        {
            HideSkill();
            coolTimeImage.gameObject.SetActive(false);
        }
        // 저장된 스킬이 있는 경우
        else
            Viewskill(skill);

        if(item == null)
            HideItem();
    }

    public void SetShoryKey(string key)
    {
        inputShortKey = key;
        SetShortKeyText();
    }

    public void SetItemIndex(int index)
    {
        itemIndex = index;
    }

    public void RegisterInput(ActiveSkill getSkill)
    {
        item = null;
        ActiveSkill newSkill = getSkill.SkillClone();

        skill = newSkill;

        HideItem();
        Debug.Log(shortkeyIndex + ", Get Skill : " + newSkill.name);

        if(skill != null)
        {
            Viewskill(skill);

            shortKeyManager.InputSkillInShortkey(shortkeyIndex);
        }
    }

    public void RegisterInput(int index)
    {
        skill = null;
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
        skillImgeSlot.sprite = activeSkill.SkillIcon;

        // 스킬을 사용할 수 없고, 스킬의 남은 쿨타임이 0.0f보다 크다면
        if (!skill.CanUseSkill && skill.CurrentRemainCoolTimer > 0.0f)
        {
            coolTimeImage.gameObject.SetActive(true);
            coolTimer = skill.CurrentRemainCoolTimer;
            StartCoroutine(CoolTimer(skill.skillCoolTime));
        }
        else
            coolTimeImage.gameObject.SetActive(false);
    }

    private void ViewItem()
    {
        viewPort.SetActive(true);
        itemIcon.sprite = item.itemImage;
        itemCnt.text = ItemCount().ToString();
    }

    private void UseSkill()
    {
        if (!skill.CanUseSkill)
            return;
        
        Manager.Instance.Game.PlayerController.UseActiveSkill(skill);

        coolTimer = skill.CurrentRemainCoolTimer;
        StartCoroutine(CoolTimer(skill.skillCoolTime));
    }

    private void UseItem()
    {
        InvenData.instance.UsingInvenItem(item);

        itemCnt.text = ItemCount().ToString();
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

    // UI에 표시되는 단축키
    private void SetShortKeyText()
    {
        // 단축키를 받아와서 string으로 변환
        string code = Manager.Instance.Key.GetKeyCode(inputShortKey).ToString();

        // 단축키가 숫자일시, "Alpha"가 들어가기에 제거
        if (code.Contains("Alpha"))
        {
            code = code.Replace("Alpha","");
        }

        shortkeyNumber.text = code;
    }

    public InvenItem GetItem()
    {
        return item;
    }

    public ActiveSkill GetSkill()
    {
        return skill;
    }

    IEnumerator CoolTimer(float timer)
    {
        coolTimeImage.gameObject.SetActive(true);
        coolTimeImage.fillAmount = 1.0f;

        float imageCoolTime = timer;
        while(coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            coolTimeImage.fillAmount = coolTimer / imageCoolTime;

            yield return null;
        }

        coolTimeImage.gameObject.SetActive(false);
    }
}
