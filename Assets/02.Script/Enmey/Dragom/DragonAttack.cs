using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : EnemyAttack
{
    public GameObject RemainFlame;
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    Transform RemainFlamePos;
    protected override void Start()
    {
        base.Start();
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[ps.main.maxParticles];
        int collisionNum = ps.GetCollisionEvents(other, collisionEvents);

        if (other.CompareTag("PlayerComponent"))
        {
            other.GetComponent<PlayerStatus>().TakeDamage(this.GetComponentInParent<EnemyStatus>().AttackDamage);
        }

        if (other.CompareTag("Ground"))
        {
            if (collisionNum > 0)
            {
                for (int i = 0; i < collisionNum; i++)
                {
                    // �ʹ� ���� ���� �ʿ�
                    Vector3 collisionPos = collisionEvents[i].intersection;
                    if (i != 0 && Vector3.Distance(collisionPos, collisionEvents[i - 1].intersection) >= 2.0f)
                    {
                        Instantiate(RemainFlame, collisionPos, Quaternion.identity);
                    }                    
                }
            }
        }
    }

    // Fail
    private void OnParticleTrigger()
    {
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < enter.Count; i++)
        {
            Vector3 psColliderPos = enter[i].position;

            Collider[] colliders = Physics.OverlapSphere(psColliderPos, 1.0f);

            for (int j = 0; j < colliders.Length; j++)
            {

                if (colliders[j].CompareTag("Player"))
                {
                    colliders[j].GetComponentInParent<PlayerStatus>().TakeDamage(this.GetComponentInParent<EnemyStatus>().AttackDamage);
                }
            }
        }
        // other.GetComponent<PlayerStatus>().TakeDamage(this.GetComponentInParent<EnemyStatus>().AttackDamage);
    }
}