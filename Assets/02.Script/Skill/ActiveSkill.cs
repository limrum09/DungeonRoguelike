using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SkillWeaponValue
{
    Public,
    OneHandSword,
    TwoHandSword,
    Spear,
    Magic
}

[CreateAssetMenu(menuName = "Skill/ActiveSkill", fileName = "Skill_")]
public class ActiveSkill : ScriptableObject
{
    [Header("Saveing Data")]
    // 에니메이션 이름_무기 번호
    [SerializeField]
    private string skillCode;
    [SerializeField]
    private int skillLevel;
    [SerializeField]
    private int maxLevel;

    [Header("Info")]
    [SerializeField]
    private string skillName;
    [SerializeField]
    private SkillWeaponValue weapon;
    [SerializeField]
    private string animationName;
    [SerializeField]
    private Sprite icon;
    [TextArea]
    [SerializeField]
    private string skillInfo;

    [Header("Option")]
    [SerializeField]
    private int needPlayerLevel;
    [SerializeField]
    private ActiveSkillCondition[] conditions;

    public string SkillCode => skillCode;
    public int CurrentSkillLevel => skillLevel;
    public int MaxSkillLeven => maxLevel;

    public string SkillName => skillName;
    public SkillWeaponValue WeaponValue => weapon;
    public string AnimationName => animationName;
    public Sprite SkillIcon => icon;
    public string SkillInfo => skillInfo;

    public float coolTime;
    private float rightWeaponValue;

    public int NeedPlayerLevel => needPlayerLevel;
    public ActiveSkillCondition[] Conditions => conditions;
    public bool NeedSkillCondition {
        get {

            bool check = true;

            if (conditions != null)
                check = conditions.All(x => x.IsSkillPass());

            return check;
        }        
    }

    public bool NeedLevelCondition => needPlayerLevel <= Manager.Instance.Game.Level;
    public float RightWeaponValue
    {
        get
        {
            rightWeaponValue = 0f;
            switch (weapon)
            {
                case SkillWeaponValue.OneHandSword:
                    rightWeaponValue = 1;
                    break;
                case SkillWeaponValue.TwoHandSword:
                    rightWeaponValue = 3;
                    break;
                case SkillWeaponValue.Spear:
                    rightWeaponValue = 4;
                    break;
                case SkillWeaponValue.Magic:
                    rightWeaponValue = 5;
                    break;
            }

            return rightWeaponValue;
        }
    }

    public void SkillLevelUp()
    {
        if (skillLevel >= maxLevel)
            return;

        skillLevel++;
        // 다른 것들 추가 필요
    }

    public ActiveSkill SkillClone() => this;
}
