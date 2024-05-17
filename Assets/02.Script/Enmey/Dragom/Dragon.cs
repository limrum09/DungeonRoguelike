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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = dragonStatus.WalkSpeed;
        base.EnemyMove();
    }
}
