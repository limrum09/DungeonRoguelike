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
    [SerializeField]
    private TextMeshProUGUI timerText;

    public string InputShortKey => inputShortKey;

    private float coolTimer;
    public int ItemIndex
    {
        get
        {
            int index = -1;

            if (item != null)
                index = itemIndex;

            return index;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(item != null || skill != null)
        {
            if (string.IsNullOrEmpty(inputShortKey))
                return;

            if (Input.GetKeyDown(Manager.Instance.Key.GetKeyCode(inputShortKey)))
            {
                if (item != null)
                {
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

    // 단축키에 추가된 스킬
    public void RegisterInput(ActiveSkill getSkill)
    {
        // 스킬 클론 생성
        ActiveSkill newSkill = getSkill.SkillClone();
        skill = newSkill;

        HideItem();

        if(skill != null)
        {
            Viewskill(skill);

            shortKeyManager.InputSkillInShortkey(shortkeyIndex);
        }
    }

    // 단축키에 추가된 아이템
    public void RegisterInput(int index)
    {
        itemIndex = index;

        // InvenData의 아이템 할당
        item = InvenData.instance.invenSlots[itemIndex];

        HideSkill();

        if(item != null)
        {
            ViewItem();

            // 다른 단축키에서 추가하는 아이템이 있을 경우, 다른 단축키의 해당 아이템 제거
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
        itemIndex = -1;
        itemIcon.sprite = null;
        itemCnt.text = "";
        viewPort.SetActive(false);
    }

    public void RefreshItemCnt()
    {
        itemCnt.text = ItemCount().ToString();
    }

    public InvenItem GetItem()
    {
        return item;
    }

    public ActiveSkill GetSkill()
    {
        return skill;
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
        itemIcon.sprite = item.ItemImage;
        itemCnt.text = ItemCount().ToString();
    }

    private void UseSkill()
    {
        if (!skill.CanUseSkill)
            return;

        if (!Manager.Instance.Game.PlayerController.CanUseSkill)
            return;

        Manager.Instance.Game.PlayerController.InputActiveSkill(skill);

        StartCoroutine(CoolTimer(skill.skillCoolTime));
    }

    private void UseItem()
    {
        InvenData.instance.UsingInvenItem(item);
    }

    // 인벤토리에 있는 해당하는 전체 아이템의 개수
    private int ItemCount()
    {
        int count = 0;
        string itemName = item.ItemName;

        foreach(var inven in InvenData.instance.invenSlots)
        {
            if(inven != null && item != null)
            {
                if (inven.ItemName == itemName)
                    count += inven.ItemCnt;
            }
        }

        return count;
    }

    // UI에 표시되는 단축키
    private void SetShortKeyText()
    {
        // 단축키를 받아와서 string으로 변환
        string code = Manager.Instance.Key.GetKeyCode(inputShortKey, true).ToString();

        // 단축키가 숫자일시, "Alpha"가 들어가기에 제거
        if (code.Contains("Alpha"))
        {
            code = code.Replace("Alpha","");
        }

        shortkeyNumber.text = code;
    }

    IEnumerator CoolTimer(float timer)
    {
        coolTimeImage.gameObject.SetActive(true);
        coolTimeImage.fillAmount = 1.0f;
        coolTimer = skill.CurrentRemainCoolTimer;

        float imageCoolTime = timer;
        while(coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            coolTimeImage.fillAmount = coolTimer / imageCoolTime;
            timerText.text = Mathf.RoundToInt(coolTimer).ToString();

            yield return null;
        }

        coolTimeImage.gameObject.SetActive(false);
    }
}
