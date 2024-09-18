using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : EnemyStatus
{
    [SerializeField]
    private BossCheckPhase checkPhase;
    private void Awake()
    {
        currentHP = maxHP;
        SetBossStatus();
    }
    
    private void SetBossStatus()
    {
        float hpPer = currentHP / maxHP;

        if(0.31f <= hpPer && hpPer <= 0.60f)
        {
            int addAttackDamage = (attackDamage * 20) / 100;
            attackDamage += addAttackDamage;
            walkSpeed = walkSpeed * 1.2f;

            checkPhase.ChangeBossPhase(2);
        }
        else if(hpPer <= 0.30f)
        {
            int addAttackDamage = (attackDamage * 30) / 100;
            attackDamage += addAttackDamage;
            walkSpeed = walkSpeed * 1.3f;

            checkPhase.ChangeBossPhase(3);
        }
    }
}
