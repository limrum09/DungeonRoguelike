using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPhase : MonoBehaviour
{
    [SerializeField]
    private BossEnemy bossEnemy;

    public void ChangeBossPhase(int i) => bossEnemy.ChangeBossPhase(i);
}
