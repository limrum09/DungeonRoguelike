using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : EnemyStatus
{
    [SerializeField]
    private BossCheckPhase checkPhase;

    protected override void Awake()
    {
        currentHP = maxHP;
    }

    protected override void Start()
    {
        base.Start();

        SetBossStatus();
    }

    protected new void EnemyHit()
    {
        base.EnemyHit();

        SetBossStatus();
    }
    
    // 보스 체력과 페이즈 확인
    private void SetBossStatus()
    {
        float hpPer = currentHP / maxHP;    // 체력 비율
        int currnetBossPhase = 1;           // 보스 페이즈

        // 2페이즈 
        if(0.31f <= hpPer && hpPer <= 0.60f)
        {
            // 공격력 증가
            int addAttackDamage = (attackDamage * 20) / 100;
            attackDamage += addAttackDamage;
            // 이동속도 증가
            walkSpeed = walkSpeed * 1.2f;

            currnetBossPhase = 2;
        }
        // 3페이즈, 이하동일
        else if(hpPer <= 0.30f)
        {
            int addAttackDamage = (attackDamage * 30) / 100;
            attackDamage += addAttackDamage;
            walkSpeed = walkSpeed * 1.3f;

            currnetBossPhase = 3;
        }

        // 페이즈 변경, 피해를 입을 때 마다 한번씩 확인하며 호출
        checkPhase.ChangeBossPhase(currnetBossPhase);
    }
}
