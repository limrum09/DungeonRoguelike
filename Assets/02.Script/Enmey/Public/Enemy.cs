using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Transform player;
    [SerializeField]
    protected Transform spawnPosition;
    [SerializeField]
    protected Transform target;
    protected NavMeshAgent nmAgent;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected SphereCollider attackAreaCollider;

    [SerializeField]
    protected bool checkPlayer;
    protected bool useSkill;
    public bool hitEnemy;

    private bool checkMosterArea;
    protected float attackAreaRadius;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        checkMosterArea = false;
        checkPlayer = true;
        hitEnemy = false;
        useSkill = false;

        nmAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = spawnPosition;

        attackAreaRadius = attackAreaCollider.radius;

        if(GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!animator.GetBool("Die") && !hitEnemy)
        {
            if (!checkPlayer)
            {
                EnemyMove();
            }
        }
    }

    public void StaySpawnPosition()
    {
        animator.SetBool("Attack", false);
        animator.SetFloat("Forward", 0);
    }

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
        Debug.Log("Detect : " + detect + ", Check :" + checkMosterArea);
        if (detect)
        {
            Debug.Log("Detect Player! " + checkMosterArea);
            if (checkMosterArea)
            {
                Debug.Log("Attack Player");
                target = player;
            }
            else
            {
                target = spawnPosition;
            }
            
            checkPlayer = false;
        }
        else
        {
            Debug.Log("Lost Player");
            target = spawnPosition;
            checkPlayer = true;
        }
    }

    // Follow the Player
    protected virtual void EnemyMove()
    {
        animator.SetBool("Attack", false);
        animator.SetFloat("Forward", 1);

        // 거리구하기
        Vector3 normalizedDirectionToTarget = (target.position - this.transform.position).normalized;
        Vector3 targetPosition = target.position - ( normalizedDirectionToTarget * attackAreaRadius );

        nmAgent.SetDestination(targetPosition);
    }

    // Rotate to Player
    protected virtual void LookPlayer()
    {
        nmAgent.updateRotation = false;

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
            Debug.Log("가까움");
            return;
        }
            
    }

    protected virtual void Attackanimation()
    {
        animator.SetFloat("Forward", 0);
        animator.SetBool("Attack", true);
    }

    public void EnemyAttack()
    {
        checkPlayer = true;
        Attackanimation();
    }

    public void EnemyMoveToPlayer()
    {
        checkPlayer = false;
    }

    public void SkillPlayState(bool state)
    {
        useSkill = state;
    }
}
