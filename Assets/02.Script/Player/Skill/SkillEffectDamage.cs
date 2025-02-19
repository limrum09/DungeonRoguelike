using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectDamage : MonoBehaviour
{
    private int skillDamage = 0;
    
    public void SetSkillDamage(int damage)
    {
        skillDamage = damage;
    }

    public void TakeDamageToEnemy(GameObject other)
    {
        other.GetComponentInParent<EnemyStatus>().TakeDamage(SetTakeSkillDamage());
        Debug.Log("스킬 데미지 : " + skillDamage);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision : " + other.name);
        if (other.CompareTag("Enemy"))
        {
            TakeDamageToEnemy(other);
        }
    }

    // 스킬 데미지 계산
    private int SetTakeSkillDamage()
    {
        // 플레이어의 스테이터스 가져오기
        var status = PlayerInteractionStatus.instance;

        // 기본 데미지
        int returnDamage = skillDamage + status.PlayerDamage;

        // 크리티컬 확율
        float critical = Random.Range(0.0f, 100.0f);

        // 크리티컬 데미지
        if (critical <= status.CriticalPer)
        {
            int skillCriticalDamage = status.CriticalDamage;

            returnDamage += skillCriticalDamage;
        }

        return returnDamage;
    }
}
