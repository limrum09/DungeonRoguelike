using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected Transform target;
    protected NavMeshAgent nmAgent;
    protected Animator animator;

    protected bool checkPlayer;
    public bool hitEnemy;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        checkPlayer = false;
        hitEnemy = false;
        nmAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!animator.GetBool("Die") && !hitEnemy)
        {
            if (!checkPlayer)
            {
                EnemyMove();
            }
            LookPlayer();
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

        if (angleChange.y > 0)
        {
            animator.SetFloat("Rotation", -1);
        }
        else if (angleChange.y < 0)
        {
            animator.SetFloat("Rotation", 1);
        }
        else if (angleChange.y == 0)
        {
            animator.SetFloat("Rotation", 0);
        }
    }

    protected virtual void Attackanimation()
    {
        animator.SetFloat("Rotation", 0);
        animator.SetFloat("Forward", 0);
        animator.SetBool("Attack", true);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPlayer = true;
            Attackanimation();
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPlayer = true;
            Attackanimation();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPlayer = false;
        }
    }

    public void EnemyHitMove()
    {
        Vector3 direction = -transform.forward;

        nmAgent.Move(direction * 2.0f);
    }
}
