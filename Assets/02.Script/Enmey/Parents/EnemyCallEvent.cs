using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallEvent : MonoBehaviour
{
    private Animator enemyAnimator;
    private Enemy enemy;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    public void EnemyHitEnd()
    {
        enemyAnimator.SetBool("Hit", false);
    }

    public void EnemyDie()
    {
        enemyAnimator.SetBool("Die", false);
        Destroy(this.gameObject);
    }

    public void SkeletonAttackComboStart()
    {
        enemyAnimator.SetBool("ComboAttack", true);
    }

    public void SkeletonAttackComboEnd()
    {
        enemyAnimator.SetBool("ComboAttack", false);
    }

    public void EnmeyAttackStart()
    {
        enemy.hitEnemy = true;
    }

    public void EnmeyAttackEnd()
    {
        enemy.hitEnemy = false;
    }
}
