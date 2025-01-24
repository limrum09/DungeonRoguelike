using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    [SerializeField]
    private ActiveSkill skill;

    protected int skillDamage;
    protected int basicDamage;

    public void SetEnemySkillDamage()
    {
        basicDamage = 0;

        SetSkillDamage();
    }

    public void SetPlayerSkillDamage()
    {
        basicDamage = SetPlayerCriticalDamage();

        SetSkillDamage();
    }

    private void SetSkillDamage()
    {
        skillDamage = skill.SkillDamage + basicDamage;
    }

    private int SetPlayerCriticalDamage()
    {
        var status = PlayerInteractionStatus.instance;

        int returnDamage = status.PlayerDamage;

        float critical = Random.Range(0.0f, 100.0f);

        if (critical <= status.CriticalPer)
        {
            int skillCriticalDamage = status.CriticalDamage;

            returnDamage += skillCriticalDamage;
        }

        return returnDamage;
    }
}
