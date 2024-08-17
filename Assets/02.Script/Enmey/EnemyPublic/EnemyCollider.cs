using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField]
    private Enemy currentEnemy;

    public void CheckMosterArea(bool isArea) => currentEnemy.CheckMonsterArea(isArea);
    public void DetectPlayer(bool detect) => currentEnemy.DetectPlayer(detect);
}
