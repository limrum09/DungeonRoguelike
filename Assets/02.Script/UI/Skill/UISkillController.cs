using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillController : MonoBehaviour
{
    private ActiveSkill currentSkill;
    [Header("Skill Info")]
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private TextMeshProUGUI skillName;
    [SerializeField]
    private TextMeshProUGUI skillInfo;
    [SerializeField]
    private TextMeshProUGUI skillConditionInfo;

    [Header("Skill Point")]
    [SerializeField]
    private TextMeshProUGUI skillPointText;
    [SerializeField]
    private Button skillLevelUpBtn;

    public void SelectSkill(ActiveSkill skill)
    {
        bool checkCondition = true;
        bool needConditions = false;

        currentSkill = skill;

        skillImage.sprite = currentSkill.icon;
        skillName.text = currentSkill.skillName;
        skillInfo.text = currentSkill.skillInfo;

        string conditionText = null;

        if (!currentSkill.NeedLevelCondition)
        {
            checkCondition = false;
            conditionText += $"레벨 {currentSkill.NeedPlayerLevel}이상";
            needConditions = true;
        }

        if (!currentSkill.NeedSkillCondition)
        {
            checkCondition = false;
            int cnt = currentSkill.Conditions.Length;

            for(int i = 0; i < cnt; i++)
            {
                if (needConditions)
                    conditionText += ",";

                ActiveSkillCondition condition = currentSkill.Conditions[i];
                conditionText += $" 스킬 '{condition.NeedActiveSkillName}' 레벨 {condition.NeedSkillLevel}이상";
                needConditions = true;
            }
        }

        if (!checkCondition)
            skillConditionInfo.text = conditionText;
        else
            skillConditionInfo.text = "";
    }

    public void SkillLevelUp()
    {
        if (currentSkill == null)
            return;

        // 스킬 포인트 감소

        // 스킬 레벨업
        currentSkill.SkillLevelUp();
    }
}
