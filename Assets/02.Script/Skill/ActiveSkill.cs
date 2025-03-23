using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SkillWeaponValue
{
    Public,
    OneHandSword,
    TwoHandSword,
    DoubleSword,
    Sheild,
    Spear,
    Magic
}

public enum SkillEffectPos
{
    RightHand,
    LeftHand,
    Weapon,
    Top,
    Chest,
    Body,
    Foot,
    Ground
}

[CreateAssetMenu(menuName = "Skill/ActiveSkill", fileName = "Skill_")]
public class ActiveSkill : ScriptableObject
{
    [Header("Saveing Data")]
    // 에니메이션 이름_무기 번호
    [SerializeField]
    private string skillCode;       // 스킬 코드
    [SerializeField]
    private int skillLevel;
    [SerializeField]
    private int maxLevel;

    [Header("Skill Damage Info")]
    [SerializeField]
    private int skillBasicDamage;
    [SerializeField]
    private int skillLevelUpDamage;
    [SerializeField]
    private Vector3 skillPosition;
    [SerializeField]
    private Vector3 skillRotation;
    [SerializeField]
    private float skillMoveRange;
    [SerializeField]
    private Vector3 skillDamageRange;
    [SerializeField]
    private ParticleSystem skillEffect;
    [SerializeField]
    private SkillEffectPos skillEffectPos = SkillEffectPos.Body;
    [SerializeField]
    private float skillMaintenanceTime;
    [SerializeField]
    private float skillMoveTime = 0f;

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
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool targeting;
    [SerializeField]
    private bool isViewRotation;


    private float coolTimer;
    private bool canUseSkill = true;

    [Header("Option")]
    [SerializeField]
    private int needPlayerLevel;
    [SerializeField]
    private ActiveSkillCondition[] conditions;

    public string SkillCode => skillCode;
    public int CurrentSkillLevel => skillLevel;
    public int MaxSkillLeven => maxLevel;
    public bool CanMove => canMove;
    public bool CanUseSkill
    {
        get
        {
            canUseSkill = true;
            if (coolTimer > 0.0f)
            {
                Manager.Instance.UIAndScene.Notion.SetNotionText($"{skillName} 쿨타임...{(int)coolTimer}초");
                canUseSkill = false;
            }

            return canUseSkill;
        }
    }
    public float SkillMaintenanceTime => skillMaintenanceTime;
    public float SkillMoveTime => skillMoveTime;

    public Vector3 SkillPosition => skillPosition;
    public Vector3 SkillRotation => skillRotation;
    public Vector3 SkillDamageRange => skillDamageRange;
    public float SkillMoveRange => skillMoveRange;
    public ParticleSystem SkillEffect => skillEffect;
    public SkillEffectPos SkillEffectPosition => skillEffectPos;

    public string SkillName => skillName;
    public SkillWeaponValue WeaponValue => weapon;
    public string AnimationName => animationName;
    public Sprite SkillIcon => icon;
    public int SkillDamage => skillBasicDamage + (skillLevelUpDamage * skillLevel);
    public string SkillInfo => skillInfo;

    public float skillCoolTime;
    public bool Targeting => targeting;
    public bool ViewRotation => isViewRotation;


    public float CurrentRemainCoolTimer => coolTimer;
    private float rightWeaponValue;
    private float leftWeaponValue;
    
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
                case SkillWeaponValue.DoubleSword:
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

    public float LeftWeaponValue
    {
        get
        {
            if (weapon == SkillWeaponValue.Sheild)
                leftWeaponValue = 2;
            else if (weapon == SkillWeaponValue.DoubleSword)
                leftWeaponValue = 1;
            else
                leftWeaponValue = 0;

            return leftWeaponValue;
        }
    }

    public bool SkillLevelUp()
    {
        if (skillLevel >= maxLevel)
            return false;
        else
        {
            skillLevel++;
            return true;
        }
        // 다른 것들 추가 필요
    }

    public void UseSkill()
    {
        if (canUseSkill)
        {
            canUseSkill = false;

            float coolTimeStatus = PlayerInteractionStatus.instance.SkillCoolTime;

            if (coolTimeStatus > 50.0)
                coolTimeStatus = 50.0f;

            coolTimer = (skillCoolTime * ( 1 - (coolTimeStatus / 100)));
            Manager.Instance.StartCoroutine(SkillCoolTimer());
        }
        else
            Debug.LogError("스킬 사용 오류....");
    }

    public ActiveSkill SkillClone() => this;

    public void CheckCoolTimeOnStart()
    {
        if (coolTimer >= 0.0f)
            Manager.Instance.StartCoroutine(SkillCoolTimer());
    }

    IEnumerator SkillCoolTimer()
    {
        while(coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            yield return null;
        }
        
        canUseSkill = true;
    }
}
