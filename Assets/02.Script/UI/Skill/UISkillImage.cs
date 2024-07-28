using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillImage : MonoBehaviour
{
    [SerializeField]
    private ActiveSkill skill;
    [SerializeField]
    GameObject line;

    private Image icon;
    private Button btn;

    private void Start()
    {
        icon = GetComponent<Image>();
        btn = GetComponent<Button>();

        if (skill == null)
        {
            if(line != null)
                line.SetActive(false);

            this.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = skill.icon;

            if(line != null)
            {
                if (skill.Conditions.Length == 0)
                    line.SetActive(false);
            }
        }

        btn.onClick.AddListener(OnClickBtn);
    }

    private void OnClickBtn()
    {
        UIAndSceneManager.instance.SelectSkill(skill);
    }
}
