using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{
    private GolemStatus golemStatus;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        golemStatus = GetComponent<GolemStatus>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = golemStatus.WalkSpeed;
        base.EnemyMove();
    }
}