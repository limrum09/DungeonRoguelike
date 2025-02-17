using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillDamage : SkillDamage
{
    [Header("Normal")]
    private float damageTimer;      // 공격 시간 타이머, damageDelayTimer와 상호작용
    [SerializeField]
    private float damageDelayTimer; // 공격 딜레이 타이머, 파티클이 생성된 이후 피해를 주는 시간에 딜레이 넣기 가능. 해당 값이 0이라면 즉시 공격
    [SerializeField]
    private int attackCnt = 1;      // 생성된 파티클의 공격 횟수
    [SerializeField]
    private bool isEnterToDamage;   // OnTriggetEnter에서 피해를 줄 것인지 아닌지 설정, Stay에서 피해주는 것은 대부분 범위 공격

    [Header("DOT")]
    [SerializeField]
    private bool isDot = false;     // 토트 피해를 입히는지 확인
    [SerializeField]
    private float dotTime = 0.0f;   // 사실 아직 도트는 조금더 해봐야 함
    private float dotTimer;


    private bool isAttack = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent") && isEnterToDamage)
        {
            if (!isAttack && attackCnt > 0)
            {
                attackCnt--;            // 공격 가능 횟수 1감소, 파티클이 생성되면서 해당 파티클의 공격 가능 횟수가 정해져 있다.
                SetEnemySkillDamage();  // 부모 클래스에서 데미지 계산
                PlayerInteractionStatus.instance.TakeDamage(skillDamage);   // 데이지 주기
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerComponent") && !isEnterToDamage)
        {
            // 도트 타이머
            if (isDot)
                dotTimer += Time.deltaTime;

            // 공격 타이머
            if(attackCnt > 0)
                damageTimer += Time.deltaTime;

            // 도트 피해
            if(isDot && dotTimer >= dotTime)
            {
                dotTimer = 0.0f;
            }

            // 일반 피해
            if(damageTimer >= damageDelayTimer && !isAttack && attackCnt > 0)
            {
                damageTimer = 0.0f;     // 공격 타이머 초기화
                SetEnemySkillDamage();  // 부모 클래스에서 데미지 계산
                attackCnt--;            // 공격 가능 횟수 1감소, 파티클이 생성되면서 해당 파티클의 공격 가능 횟수가 정해져 있다.

                // 공격 횟수 모두 소진 시, 피해를 입힐 수 없음
                if(attackCnt <= 0)
                    isAttack = true;

                // 데이지 주기
                PlayerInteractionStatus.instance.TakeDamage(skillDamage);
            }
        }
    }
}
