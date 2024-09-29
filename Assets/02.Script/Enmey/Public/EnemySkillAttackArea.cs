using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillAttackArea : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.SkillPlayState(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.SkillPlayState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.SkillPlayState(false);
    }
}
