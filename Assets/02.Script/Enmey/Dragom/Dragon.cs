using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    private DragonStatus dragonStatus;
    protected override void Start()
    {
        base.Start();
        dragonStatus = GetComponent<DragonStatus>();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = dragonStatus.WalkSpeed;
        base.EnemyMove();
    }
}
