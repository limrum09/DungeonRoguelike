using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField]
    private SkillEffect prefab;
    [SerializeField]
    private Transform weaponEffectRightHandPos;
    [SerializeField]
    private Transform weaponEffectLeftHandPos;
    [SerializeField]
    private Transform weaponEffectPos;
    [SerializeField]
    private Transform topEffectPos;
    [SerializeField]
    private Transform chestEffectPos;
    [SerializeField]
    private Transform bodyEffectPos;
    [SerializeField]
    private Transform footEffectPos;
    [SerializeField]
    private Transform groundEffectPos;
    
    public void PlayBossEffect(ActiveSkill skill)
    {
        if (skill.SkillEffect == null)
            return;

        Transform effectPos = null;

        Debug.Log("스킬 이펙트 생성 시작");

        switch (skill.SkillEffectPosition)
        {
            case SkillEffectPos.RightHand:
                effectPos = weaponEffectRightHandPos;
                break;
            case SkillEffectPos.LeftHand:
                effectPos = weaponEffectLeftHandPos;
                break;
            case SkillEffectPos.Weapon:
                effectPos = weaponEffectPos;
                break;
            case SkillEffectPos.Top:
                effectPos = topEffectPos;
                break;
            case SkillEffectPos.Chest:
                effectPos = chestEffectPos;
                break;
            case SkillEffectPos.Body:
                effectPos = bodyEffectPos;
                break;
            case SkillEffectPos.Foot:
                effectPos = footEffectPos;
                break;
            case SkillEffectPos.Ground:
                effectPos = groundEffectPos;
                break;
        }

        SkillEffect newSkillEffect = Instantiate(prefab, effectPos);
        Debug.Log(newSkillEffect.name + "을 " + effectPos + " 위치에 생성");

        newSkillEffect.ActiveSkillEffect(skill);
    }
}
