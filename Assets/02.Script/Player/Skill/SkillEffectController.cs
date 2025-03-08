using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectController : MonoBehaviour
{
    [SerializeField]
    private SkillEffect prefab;
    [SerializeField]
    private List<SkillEffect> skillEffects;

    [Header("Effect Position")]
    [SerializeField]
    private Transform bodyTf;
    [SerializeField]
    private Transform chestTf;
    [SerializeField]
    private Transform rightHandTf;
    [SerializeField]
    private Transform leftHandTf;
    [SerializeField]
    private Transform footTf;
    [SerializeField]
    private Transform groundTf;
    [SerializeField]
    private Transform nonTargetingMagicTf;
    [SerializeField]
    private Transform targetingTf;

    // 한번에 여러개의 이펙트를 사용할 수 있기에 자식으로 따로 만듬
    public void ActiveSkillEffect(ActiveSkill skill, Transform tf = null)
    {
        if (skill == null)
            return;

        SkillEffect newSkillEffect = null;

        if (!skill.Targeting)
        {
            Transform instantiateTf = this.gameObject.transform;

            switch (skill.SkillEffectPosition)
            {
                case SkillEffectPos.Body :
                    instantiateTf = bodyTf;
                    break;
                case SkillEffectPos.Chest:
                    instantiateTf = chestTf;
                    break;
                case SkillEffectPos.Foot:
                    instantiateTf = footTf;
                    break;
                case SkillEffectPos.Ground:
                    instantiateTf = groundTf;
                    break;
                case SkillEffectPos.LeftHand:
                    instantiateTf = leftHandTf;
                    break;
                case SkillEffectPos.RightHand:
                    instantiateTf = rightHandTf;
                    break;
                case SkillEffectPos.Weapon:
                    instantiateTf = rightHandTf;
                    break;
            }
            newSkillEffect = Instantiate(prefab, instantiateTf);
        }
        else
        { 
            // ViewRotation이 false인 경우, tf의 위치로 newSkillEffect 옮기기 (불기둥 등)
            if (!skill.ViewRotation)
            {
                newSkillEffect = Instantiate(prefab);

                newSkillEffect.transform.position = targetingTf.position;
            }
            // 아이스 스피어 등
            else
            {
                newSkillEffect = Instantiate(prefab, nonTargetingMagicTf);
            }
        }

        if(newSkillEffect != null)
            newSkillEffect.ActiveSkillEffect(skill);
    }
}