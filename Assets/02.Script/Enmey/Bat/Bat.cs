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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
        nmAgent.speed = batStatus.WalkSpeed;
    }

    protected override void LookPlayer()
    {
        base.LookPlayer();
    }

}
