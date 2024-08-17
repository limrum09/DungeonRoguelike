using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{
    [SerializeField]
    public Enemy enemy;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy.StaySpawnPosition();
        }        
    }
}
