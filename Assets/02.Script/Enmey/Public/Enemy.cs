using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Transform player;                     // Enemy가 따라가는 Plyaer
    [SerializeField]
    protected Transform spawnPosition;              // Enemy 스폰 포인트
    [SerializeField]
    protected Transform target;                     // Enemy 목표, player나 spawnPosition 둘 중 하나만 선택 된다.
    protected NavMeshAgent nmAgent;                 // Nav Mesh Agent, 장해물 피해서 따라가는 component
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected SphereCollider attackAreaCollider;    // 공격 범위 Collider

    [SerializeField]
    protected bool checkPlayer;                     // 플레이어를 찾았는지 확인
    protected bool useSkill;                        // 스킬 사용하는지 확인(주로 원거리들)
    public bool hitEnemy;                           // 공격하는지 확인

    protected bool checkMosterArea;                 // Monster Area안에 있으야지만 Player를 쫒아가서 공격한다. 해당 지역을 벗어나면 공격을 중단하고 다시 spawnPosition으로 이동한다.
    protected float attackAreaRadius;               // 공격 범위

    // Start is called before the first frame update
    protected virtual void Start()
    {
        checkMosterArea = true;
        checkPlayer = true;
        hitEnemy = false;
        useSkill = false;

        nmAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = spawnPosition;

        attackAreaRadius = attackAreaCollider.radius;

        // player를 찾아서 값을 준다.
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!animator.GetBool("Die") && !hitEnemy)
        {
            if (!checkPlayer)
            {
                EnemyMove();
            }
        }
    }

    // 플레이어 따라가기
    protected virtual void EnemyMove()
    {
        animator.SetBool("Attack", false);
        animator.SetFloat("Forward", 1);

        // 거리구하기
        Vector3 normalizedDirectionToTarget = (target.position - this.transform.position).normalized;
        Vector3 targetPosition = target.position - ( normalizedDirectionToTarget * attackAreaRadius );

        // 호출마다 목적지를 변경, (몬스터 속도가 빠르면 못따라갈 수 있음, 필요시 변경)
        nmAgent.SetDestination(targetPosition);
    }

    // 플레이어 바라보기
    protected virtual void LookPlayer()
    {
        nmAgent.updateRotation = false;

        // 플레이어 방향 설정
        Vector3 direction = target.position - this.transform.position;
        direction.Set(direction.x, 0, direction.z);

        if (direction.sqrMagnitude > 3f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 5f * Time.deltaTime);

            Quaternion rotationChange = targetRotation * Quaternion.Inverse(this.transform.rotation);

            Vector3 angleChange = rotationChange.eulerAngles;

            angleChange.y = Mathf.DeltaAngle(0, angleChange.y);
        }
        else
        {
            return;
        }
            
    }

    protected virtual void Attackanimation()
    {
        animator.SetFloat("Forward", 0);
        animator.SetBool("Attack", true);
    }

    // 스폰 포인트에 있는 경우 에니메이션
    public void StaySpawnPosition()
    {
        animator.SetBool("Attack", false);
        animator.SetFloat("Forward", 0);
    }

    #region Monster Detect Area
    // 몬스터가 일정 지역을 벗어난 경우 호출
    public void CheckMonsterArea(bool isArea)
    {
        checkMosterArea = isArea;
        if (!isArea)
        {
            target = spawnPosition;
        }
    }

    public void DetectPlayer(bool detect)
    {
        // 플레이어 감지
        if (detect)
        {
            // Monster 공격 가능 지역에 있는지 확인
            if (checkMosterArea)
            {
                // 공격 가능 지역이면 플레이어를 쫒아간다
                target = player;
            }
            // Monster 지역에서 벗어남
            else
            {
                // 지역에서 벗어나면 스폰 포인트로 target을 변경하고 몬스터가 돌아가도록 설정
                target = spawnPosition;
            }

            checkPlayer = false;
        }
        // 플레이어가 없음
        else
        {
            // 플레이어가 없으면 스폰 포인트로 이동
            target = spawnPosition;
            checkPlayer = true;
        }
    }
    #endregion

    #region Check Monster Attack
    // Enemy의 Trigger 범위안세 플레어가 들어오거나(Enter) 멈춰있다면(Stay) 호출
    public void EnemyAttack()
    {
        checkPlayer = true;
        Attackanimation();
        LookPlayer();
    }

    // Enmey의 Trigger 범위에서 플레이어가 빠져 나간 경우 호출
    public void EnemyLostPlayer()
    {
        checkPlayer = false;
    }
    #endregion

    // Skill Attack Area에서 스킬 사용 유무 확인
    public void SkillPlayState(bool state)
    {
        useSkill = state;
    }
}
