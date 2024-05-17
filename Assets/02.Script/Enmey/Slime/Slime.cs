using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private SlimeStatus slimeStatus;
    protected override void Start()
    {
        base.Start();
        slimeStatus = GetComponent<SlimeStatus>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = slimeStatus.WalkSpeed;
        base.EnemyMove();
    }
}
