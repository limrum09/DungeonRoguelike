using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : SkillDamage
{
    [SerializeField]
    private float damageTimer;
    [SerializeField]
    private float durationTime;

    private float durationTimer = 0.0f;
    private bool isAttack = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적 공격");
            damageTimer += Time.deltaTime;

            if (damageTimer <= 0)
                damageTimer = 0.0f;

            if (durationTimer <= 0.0f)
                durationTimer = 0.0f;

            SetPlayerSkillDamage();
            other.GetComponentInParent<EnemyStatus>().TakeDamage(skillDamage);
            if (damageTimer >= durationTime && !isAttack)
            {
                durationTimer = 0.0f;
                SetPlayerSkillDamage();
                other.GetComponentInParent<EnemyStatus>().TakeDamage(skillDamage);
            }
        }
    }
}
