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

    protected override void EnemyMove()
    {
        nmAgent.speed = turtleShellStatus.WalkSpeed;
        base.EnemyMove();
    }
}
