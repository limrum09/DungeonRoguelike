using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{
    private OrcStatus orcStatus;
    protected override void Start()
    {
        base.Start();
        orcStatus = GetComponent<OrcStatus>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = orcStatus.WalkSpeed;
        base.EnemyMove();
    }
}
