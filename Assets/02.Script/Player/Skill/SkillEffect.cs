using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    private ActiveSkill skill;
    private float skillTimer;
    private float skillMaintenanceTime;

    public void ActiveSkillEffect(ActiveSkill getSkill)
    {
        skill = getSkill;
        skillMaintenanceTime = skill.SkillMaintenanceTime;

        if (skill.SkillEffect != null)
        {
            // 파티클 생성
            ParticleSystem newSkill = Instantiate(skill.SkillEffect, this.transform);
            // 정면을 바라보도록 회전 변경
            newSkill.transform.rotation = Quaternion.LookRotation(transform.forward);
            // 이펙트마다 위치 조정
            newSkill.transform.position += skill.SkillPosition;
            // 플레이
            newSkill.Play();
        }

        if (skillMaintenanceTime != 0)
        {
            StartCoroutine(SkillMaintenance());
        }
        else
        {
            EndSkillEffect();
        }
            
    }

    private void EndSkillEffect()
    {
        Destroy(this.gameObject);
    }

    // 유지시간이 지나면 이펙트 종료
    IEnumerator SkillMaintenance()
    {
        skillTimer = 0.0f;
        while(skillMaintenanceTime >= skillTimer)
        {
            skillTimer += Time.deltaTime;

            yield return null;
        }

        EndSkillEffect();
    }
}
