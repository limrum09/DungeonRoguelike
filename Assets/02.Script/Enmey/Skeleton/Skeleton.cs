using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    private SkeletonStatus skeletonStatus;
    protected override void Start()
    {
        base.Start();
        skeletonStatus = GetComponent<SkeletonStatus>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = skeletonStatus.WalkSpeed;
        base.EnemyMove();
    }
}