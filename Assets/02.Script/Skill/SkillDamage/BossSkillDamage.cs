using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillDamage : SkillDamage
{
    [Header("Normal")]
    private float damageTimer;
    [SerializeField]
    private float damageDelayTimer;
    [SerializeField]
    private int attackCnt = 1;

    [Header("DOT")]
    [SerializeField]
    private bool isDot = false;
    [SerializeField]
    private float dotTime = 0.0f;
    private float dotTimer;


    private bool isAttack = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            if (isDot)
                dotTimer += Time.deltaTime;

            if(attackCnt > 0)
                damageTimer += Time.deltaTime;

            if (damageTimer <= -0.5f)
                damageTimer = 0.0f;

            // 도트 피해
            if(isDot && dotTimer >= dotTime)
            {
                dotTimer = 0.0f;
            }

            // 일반 피해
            if(damageTimer >= damageDelayTimer && !isAttack && attackCnt > 0)
            {
                damageTimer = 0.0f;
                SetEnemySkillDamage();
                attackCnt--;

                if(attackCnt <= 0)
                    isAttack = true;

                PlayerInteractionStatus.instance.TakeDamage(skillDamage);
            }
        }
    }
}
