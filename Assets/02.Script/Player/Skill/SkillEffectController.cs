using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectController : MonoBehaviour
{
    [SerializeField]
    private SkillEffect prefab;
    [SerializeField]
    private List<SkillEffect> skillEffects;

    // 한번에 여러개의 이펙트를 사용할 수 있기에 자식으로 따로 만듬
    public void ActiveSkillEffrct(ActiveSkill skill)
    {
        SkillEffect newSkillEffect = Instantiate(prefab, this.gameObject.transform);
        
        newSkillEffect.ActiveSkillEffect(skill);
    }
}
