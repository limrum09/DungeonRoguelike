using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    [SerializeField]
    private SkillEffect prefab;
    [SerializeField]
    private GameObject weaponEffectPos;
    [SerializeField]
    private GameObject bodyEffectPos;
    
    public void PlayBossEffect(ActiveSkill skill)
    {
        if (skill.SkillEffect == null)
            return;

        GameObject effectPos = null;

        if (skill.SkillEffectPosition == SkillEffectPos.Body)
            effectPos = bodyEffectPos;
        else if (skill.SkillEffectPosition == SkillEffectPos.Weapon)
            effectPos = weaponEffectPos;

        SkillEffect newSkillEffect = Instantiate(prefab, effectPos.transform);

        newSkillEffect.ActiveSkillEffect(skill);
    }
}
