using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected Transform player;
    [SerializeField]
    protected Transform spawnPosition;
    [SerializeField]
    protected Transform target;
    protected NavMeshAgent nmAgent;
    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected bool checkPlayer;
    public bool hitEnemy;

    private bool checkMosterArea;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        checkMosterArea = false;
        checkPlayer = true;
        hitEnemy = false;

        nmAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = spawnPosition;

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
            LookPlayer();
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
        // 거리구하기
        animator.SetBool("Attack", false);
        animator.SetFloat("Forward", 1);
        nmAgent.SetDestination(target.position - new Vector3(1.5f, 0f, 1.5f));
    }

    // Rotate to Player
    protected virtual void LookPlayer()
    {
        nmAgent.updateRotation = false;

        Vector3 direction = target.position - this.transform.position;
        direction.Set(direction.x, 0, direction.z);


        if (direction.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 5f * Time.deltaTime);

            RotationAnimation(targetRotation);
        }
    }

    protected virtual void RotationAnimation(Quaternion _targetRotation)
    {
        Quaternion rotationChange = _targetRotation * Quaternion.Inverse(this.transform.rotation);

        Vector3 angleChange = rotationChange.eulerAngles;

        angleChange.y = Mathf.DeltaAngle(0, angleChange.y);
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
}
