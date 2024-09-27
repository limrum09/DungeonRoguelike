using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField]
    private SlimeStatus slimeStatus;

    protected override void EnemyMove()
    {
        nmAgent.speed = slimeStatus.WalkSpeed;
        base.EnemyMove();
    }
}
