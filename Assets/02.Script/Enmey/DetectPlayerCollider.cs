using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollider : MonoBehaviour
{
    [SerializeField]
    private EnemyCollider enemyCollider;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 들어왔을 경우
        if (other.CompareTag("Player"))
        {
            Debug.Log("In Player");
            enemyCollider.DetectPlayer(true);
        }
    }
     
    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 나갔을 경우
        if (other.CompareTag("Player"))
        {
            Debug.Log("Out Player");
            enemyCollider.DetectPlayer(false);
        }
    }
}
