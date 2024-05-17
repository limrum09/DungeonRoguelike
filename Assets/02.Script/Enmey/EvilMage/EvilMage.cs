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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = evilMageStatus.WalkSpeed;
        base.EnemyMove();
    }
}
