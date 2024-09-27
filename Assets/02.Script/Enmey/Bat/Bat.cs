using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField]
    private BatStatus batStatus;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        batStatus = GetComponent<BatStatus>();
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
        nmAgent.speed = batStatus.WalkSpeed;
    }
}
