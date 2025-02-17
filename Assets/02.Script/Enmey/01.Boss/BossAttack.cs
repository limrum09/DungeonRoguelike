using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private EnemyStatus status;

    // 전방 충돌을 판단하는 값, 범위는 -1(완전히 뒤쪽)에서 1(완전히 앞쪽)
    // 임계값 0.5는 대략 60도(각도) 이내의 전방을 의미한다. 
    private const float frontThreshold = 0.00f;

    private void OnTriggerEnter(Collider other)
    {
        if (enemy.hitEnemy && other.CompareTag("PlayerComponent"))
        {
            // 무기와 other과 방향 계간
            Vector3 directionToOther = (other.transform.position - transform.position).normalized;
            // 무기의 전진 방향과 other사이에 점을 찍어 값을 확인
            float dot = Vector3.Dot(transform.forward, directionToOther);

            // 점이 임계값도다 크다면 충돌이 앞쪽에서 일어난 것으로 간주
            if (dot > frontThreshold)
            {
                // 피해주기
                if (other.GetComponentInParent<PlayerInteractionStatus>())
                {
                    other.GetComponentInParent<PlayerInteractionStatus>().TakeDamage(status.AttackDamage);
                }
            }
        }
    }
}
