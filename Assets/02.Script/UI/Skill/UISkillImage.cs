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
    
    private Image icon;

    public ActiveSkill Skill => skill;

    public void SkillImageStart()
    {
        icon = GetComponent<Image>();

        if (skill == null)
        {
            if(line != null)
                line.SetActive(false);

            this.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = skill.SkillIcon;
            skill.CheckCoolTimeOnStart();

            if(line != null)
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
}
