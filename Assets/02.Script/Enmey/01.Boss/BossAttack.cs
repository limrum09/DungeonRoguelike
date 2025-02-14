using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private EnemyStatus status;
    
    private void OnTriggerEnter(Collider other)
    {
        if (enemy.hitEnemy)
        {
            if(other.GetComponentInParent<PlayerInteractionStatus>())
                other.GetComponentInParent<PlayerInteractionStatus>().TakeDamage(status.AttackDamage);
        }
    }
}
