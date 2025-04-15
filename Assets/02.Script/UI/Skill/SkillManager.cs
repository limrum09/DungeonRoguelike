using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public event Action<ActiveSkill> OnSkillLevelUp;
    public event Action OnPlayerLevelUp;

    public bool CanLevelUpSkill(ActiveSkill skill)
    {
        return skill.NeedSkillCondition && skill.NeedSkillCondition && skill.CurrentSkillLevel < skill.MaxSkillLevel;
    }

    public void CheckSkillConditionToPlayerLevelUp()
    {
        OnPlayerLevelUp?.Invoke();
    }

    public bool TryLevelUpSkill(ActiveSkill skill)
    {
        if (CanLevelUpSkill(skill))
        {
            bool success = skill.SkillLevelUp();
            if (success)
            {
                Manager.Instance.Game.PlayerUseSkillPoint(1);
                OnSkillLevelUp?.Invoke(skill);
                return true;
            }
        }

        return false;
    }
}
