using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlant : Enemy
{
    private MonsterPlantStatus monsterPlantStatus;
    protected override void Start()
    {
        base.Start();
        monsterPlantStatus = GetComponent<MonsterPlantStatus>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        nmAgent.speed = monsterPlantStatus.WalkSpeed;
        base.EnemyMove();
    }
}
