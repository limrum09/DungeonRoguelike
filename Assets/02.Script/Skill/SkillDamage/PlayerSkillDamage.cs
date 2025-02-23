using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : SkillDamage
{
    private float damageTimer = 0.0f;
    [SerializeField]
    private float damageDelayTime; // 공격 딜레이 타이머, 파티클이 생성된 이후 피해를 주는 시간에 딜레이 넣기 가능. 해당 값이 0이라면 즉시 공격
    [SerializeField]
    private float loofTime;
    [SerializeField]
    private int attackCnt = 1;      // 생성된 파티클의 공격 횟수
    // OnTriggetEnter에서 피해를 줄 것인지 아닌지 설정, Stay에서 피해주는 것은 대부분 범위 공격
    // true일 경우, OntriggerEnter에서 피해를 준다.
    [SerializeField]
    private bool isEnterToDamage;

    private bool isAttack = false;

    private HashSet<Collider> enemiesInRange = new HashSet<Collider>();

    private bool isDestroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other))
            {
                enemiesInRange.Add(other);
            }

            if (isEnterToDamage && attackCnt > 0)
            {
                ApplyDamage(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other);
        }
    }

    private void Update()
    {
        if (isDestroyed) return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageDelayTime && attackCnt > 0)
        {
            damageDelayTime = loofTime;
            damageTimer = 0.0f;

            List<Collider> toRemove = new List<Collider>();

            foreach (Collider enemy in enemiesInRange)
            {
                if (enemy == null || !enemy.gameObject.activeInHierarchy)
                {
                    toRemove.Add(enemy); // 비활성화 또는 파괴된 적 제거 예정
                    continue;
                }

                ApplyDamage(enemy);
            }

            // 파괴된 적 제거
            foreach (var enemy in toRemove)
            {
                enemiesInRange.Remove(enemy);
            }

            attackCnt--;
        }
    }

    private void ApplyDamage(Collider enemy)
    {
        Debug.Log("적 공격: " + enemy.name);
        SetPlayerSkillDamage();
        enemy.GetComponentInParent<EnemyStatus>()?.TakeDamage(skillDamage);
    }

    private void OnDestroy()
    {
        isDestroyed = true;
        enemiesInRange.Clear();  // 메모리 정리
        Debug.Log("스킬 오브젝트가 파괴되었습니다.");
    }
}
