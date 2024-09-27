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

    protected override void EnemyMove()
    {
        nmAgent.speed = monsterPlantStatus.WalkSpeed;
        base.EnemyMove();
    }
}
