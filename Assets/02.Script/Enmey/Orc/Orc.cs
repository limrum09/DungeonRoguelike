using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{   
    [SerializeField]
    private OrcStatus orcStatus;
    protected override void Start()
    {
        base.Start();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = orcStatus.WalkSpeed;
        base.EnemyMove();
    }
}
