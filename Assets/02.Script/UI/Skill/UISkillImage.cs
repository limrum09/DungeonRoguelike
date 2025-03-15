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
    private Image parentImage;
    
    private Image icon;

    public ActiveSkill Skill => skill;

    public void SkillImageStart()
    {
        icon = GetComponent<Image>();
        parentImage = transform.parent.GetComponent<Image>();
        Color pColor = parentImage.color;

        if (skill == null)
        {
            if(line != null)
                line.SetActive(false);

            pColor.a = 0.0f;
            this.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = skill.SkillIcon;
            skill.CheckCoolTimeOnStart();
            pColor.a = 1.0f;
            if(line != null)
            {
                if (skill.Conditions.Length == 0)
                    line.SetActive(false);
            }
        }

        parentImage.color = pColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Manager.Instance.UIAndScene.SelectSkill(skill);
    }
}
