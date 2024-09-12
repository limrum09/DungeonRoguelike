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
        Debug.Log("Collision");
        if (other.CompareTag("Enemy"))
        {
            TakeDamageToEnemy(other);
        }
    }

    private int SetTakeSkillDamage()
    {
        var status = PlayerInteractionStatus.instance;

        int returnDamage = skillDamage + status.PlayerDamage;

        float critical = Random.Range(0.0f, 100.0f);

        if (critical <= status.CriticalPer)
        {
            int attackDamages = status.PlayerDamage + status.CriticalDamage;
            int damageMag = attackDamages / status.PlayerDamage;

            int skillCriticalDamage = skillDamage * damageMag;

            Debug.Log("Critical Mag : " + damageMag);

            skillDamage += skillCriticalDamage;
        }

        return returnDamage;
    }
}
