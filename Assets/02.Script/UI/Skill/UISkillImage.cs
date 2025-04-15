using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkillImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ActiveSkill skill;
    [SerializeField]
    GameObject line;
    [SerializeField]
    private Image isNotUseSkillFrame;
    
    private Image icon;
    private bool eventRegistered = false;

    public ActiveSkill Skill => skill;

    public void SkillImageStart(ActiveSkill _skill)
    {
        skill = _skill;
        icon = GetComponent<Image>();

        if (skill == null)
        {
            if(line != null)
                line.SetActive(false);

            isNotUseSkillFrame.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = skill.SkillIcon;
            skill.CheckCoolTimeOnStart();

            UpdateSkillAvailabilityUI(skill.NeedLevelCondition , skill.NeedSkillCondition);            

            if (skill.NeedPlayerLevel > 1 || skill.Conditions.Length > 0 && !eventRegistered)
            {
                Manager.Instance.Skill.OnSkillLevelUp += CheckSkillCondition;
                Manager.Instance.Skill.OnPlayerLevelUp += CheckSkillCondition;
                eventRegistered = true;
            }

            if (line != null)
            {
                if (skill.Conditions.Length == 0)
                    line.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Manager.Instance.UIAndScene.SelectSkill(skill);
    }

    public void CheckSkillCondition(ActiveSkill skill)
    {
        if(skill == this.skill)
            UpdateSkillAvailabilityUI(skill.NeedLevelCondition , skill.NeedSkillCondition);
    }

    public void CheckSkillCondition()
    {
        UpdateSkillAvailabilityUI(skill.NeedLevelCondition , skill.NeedSkillCondition);
    }

    private void UpdateSkillAvailabilityUI(bool levelCondtion, bool skillCondition)
    {
        isNotUseSkillFrame.gameObject.SetActive(!(levelCondtion && skillCondition));
    }

    private void OnDestroy()
    {
        if(eventRegistered)
            Manager.Instance.Skill.OnSkillLevelUp -= CheckSkillCondition;
    }
}
