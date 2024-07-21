using System.Collections;
using System.Collections.Generic;
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
    public SkillWeaponValue weapon;
    public string animationName;
    public Sprite icon;

    public float coolTime;

    private float rightWeaponValue;

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

    public ActiveSkill SkillClone() => this;
}
