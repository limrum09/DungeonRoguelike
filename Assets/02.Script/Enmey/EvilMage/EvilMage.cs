using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMage : Enemy
{
    private EvilMageStatus evilMageStatus;
    protected override void Start()
    {
        base.Start();
        evilMageStatus = GetComponent<EvilMageStatus>();
    }

    protected override void Update()
    {
        base.Update();

        if (!animator.GetBool("Die") && !hitEnemy)
        {
            LookPlayer();
        }
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = evilMageStatus.WalkSpeed;
        base.EnemyMove();
    }
}
