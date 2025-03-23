using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillController : MonoBehaviour
{
    private ActiveSkill currentSkill;
    [SerializeField]
    private List<UISkillImageController> skillImageControllers;
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

    public void SkillUIStart()
    {
        SkillUIInitialized();
        Manager.Instance.Game.PlayerCurrentStatus.OnSkillPointChanged += RefreshSkillPoint;
    }

    public void SelectSkill(ActiveSkill skill)
    {
        bool checkCondition = true;
        bool needConditions = false;

        currentSkill = skill;

        if (currentSkill != null)
            skillImage.gameObject.SetActive(true);
        else
            SkillUIInitialized();

        // 스킬 이미지, 이름, 정보
        skillImage.sprite = currentSkill.SkillIcon;
        skillName.text = $"{currentSkill.SkillName} Lv.{currentSkill.CurrentSkillLevel} / MaxLv.{currentSkill.MaxSkillLeven}";
        skillInfo.text = currentSkill.SkillInfo;

        // 스킬 조건
        string conditionText = null;

        // 스킬의 필요한 레벨이 부족할 경우
        if (!currentSkill.NeedLevelCondition)
        {
            checkCondition = false;
            conditionText += $"레벨 {currentSkill.NeedPlayerLevel}이상";
            needConditions = true;
        }

        // 선행스킬을 덜 익혔을 경우
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

        // 스킬 조건을 모두 만족하지 못 한경우 실행
        if (!checkCondition)
        {
            skillConditionInfo.text = conditionText;
            skillLevelUpBtn.interactable = false;
        }
        else
        {
            skillConditionInfo.text = "";
            skillLevelUpBtn.interactable = true;
        }

        if (currentSkill.CurrentSkillLevel >= currentSkill.MaxSkillLeven)
            skillLevelUpBtn.interactable = false;
    }

    public void SkillLevelUp()
    {
        if (currentSkill == null)
            return;

        // 스킬 레벨업
        if (currentSkill.SkillLevelUp())
        {
            // 스킬 포인트 감소
            Manager.Instance.Game.PlayerUseSkillPoint(1);
        }

        RefreshSkill();
    }

    private void SkillUIInitialized()
    {
        if (currentSkill == null)
        {
            skillImage.sprite = null;
            skillImage.gameObject.SetActive(false);

            skillName.text = "";
            skillInfo.text = "";
            skillConditionInfo.text = "";

            skillLevelUpBtn.interactable = false;
        }

        foreach (var skillImages in skillImageControllers)
        {
            if (skillImages != null)
                skillImages.UISkillImageStart();
        }
    }

    private void RefreshSkillPoint(int skPoint)
    {
        skillPointText.text = skPoint.ToString();
    }

    private void RefreshSkill()
    {
        // 스킬 이미지, 이름, 정보
        skillImage.sprite = currentSkill.SkillIcon;
        skillName.text = $"{currentSkill.SkillName} Lv.{currentSkill.CurrentSkillLevel} / MaxLv.{currentSkill.MaxSkillLeven}";
        skillInfo.text = currentSkill.SkillInfo;

        if (currentSkill.CurrentSkillLevel >= currentSkill.MaxSkillLeven)
            skillLevelUpBtn.interactable = false;
    }

    private void OnDestroy()
    {
        Manager.Instance.Game.PlayerCurrentStatus.OnSkillPointChanged -= RefreshSkillPoint;
    }
}
