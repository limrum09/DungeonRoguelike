using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMageAttack : EnemyAttack
{
    private GameObject EvilMage;

    protected override void Start()
    {
        base.Start();
        EvilMage = GameObject.FindGameObjectWithTag("EvilMage");
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerInteractionStatus>().TakeDamage(EvilMage.GetComponent<EnemyStatus>().AttackDamage);
        }
    }
}
