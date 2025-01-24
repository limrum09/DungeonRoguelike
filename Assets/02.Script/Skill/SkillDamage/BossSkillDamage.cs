using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillDamage : SkillDamage
{
    [SerializeField]
    private float damageTimer;
    [SerializeField]
    private float durationTime;

    private float durationTimer = 0.0f;
    private bool isAttack = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer <= 0)
                damageTimer = 0.0f;

            if (durationTimer <= 0.0f)
                durationTimer = 0.0f;

            if(damageTimer >= durationTime && !isAttack)
            {
                durationTimer = 0.0f;
                SetEnemySkillDamage();
                isAttack = true;

                PlayerInteractionStatus.instance.TakeDamage(skillDamage);
            }
        }
    }
}
