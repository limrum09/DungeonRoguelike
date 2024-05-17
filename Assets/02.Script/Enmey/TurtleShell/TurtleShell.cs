using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShell : Enemy
{
    private TurtleShellStatus turtleShellStatus;
    protected override void Start()
    {
        base.Start();
        turtleShellStatus = GetComponent<TurtleShellStatus>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = turtleShellStatus.WalkSpeed;
        base.EnemyMove();
    }
}
