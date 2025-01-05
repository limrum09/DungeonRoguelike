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
    public void ActiveSkillEffect(ActiveSkill skill, Transform tf = null)
    {
        if (skill == null)
            return;

        if (!skill.Targeting)
        {
            SkillEffect newSkillEffect = Instantiate(prefab, this.gameObject.transform);
            newSkillEffect.ActiveSkillEffect(skill);
        }
        else
        {
            SkillEffect newSkillEffect = null;
            Debug.Log("생성");

            // ViewRotation이 false인 경우, tf의 위치로 newSkillEffect 옮기기 (불기둥 등)
            if (!skill.ViewRotation)
            {
                newSkillEffect = Instantiate(prefab);

                newSkillEffect.transform.position = tf.position;

                newSkillEffect.ActiveSkillEffect(skill);
            }
            // 아이스 스피어 등
            else
            {
                newSkillEffect = Instantiate(prefab, this.gameObject.transform);

                // +만으로는 반대로 찍히면 캐릭터와 충돌, 좌표보고 -+ 하기
                newSkillEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                // 방향 찾기
                Vector3 direction = (tf.position - newSkillEffect.transform.position).normalized;

                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    // newSkillEffect.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f);
                    // newSkillEffect.transform.rotation = Quaternion.LookRotation(direction);
                }
            }
            newSkillEffect.ActiveSkillEffect(skill);
        }
    }
}