using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/SkillCondition", fileName = "_Condition")]
public class ActiveSkillCondition : ScriptableObject
{
    [SerializeField]
    private ActiveSkill needActiveSkill;
    [SerializeField]
    private int needSkillLevel;

    public string NeedActiveSkillName => needActiveSkill.SkillName;
    public int NeedSkillLevel => needSkillLevel;

    public bool IsSkillPass()
    {
        bool check = false;

        if (needActiveSkill.CurrentSkillLevel >= needSkillLevel)
            check = true;

        return check;
    }
}
