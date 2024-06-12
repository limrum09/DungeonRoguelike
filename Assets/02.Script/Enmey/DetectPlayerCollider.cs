using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollider : MonoBehaviour
{
    [SerializeField]
    private EnemyCollider enemyCollider;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ������ ���
        if (other.CompareTag("Player"))
        {
            Debug.Log("In Player");
            enemyCollider.DetectPlayer(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ ������ ���
        if (other.CompareTag("Player"))
        {
            Debug.Log("Out Player");
            enemyCollider.DetectPlayer(false);
        }
    }
}
