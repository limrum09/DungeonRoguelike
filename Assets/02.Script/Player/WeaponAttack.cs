using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;
        player = GameObject.FindGameObjectWithTag("PlayerComponent");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.playerState == PlayerState.Attack)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && playerController.isCombo == true && isAttack == true)
        {
            other.GetComponentInParent<EnemyStatus>().TakeDamage(GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerStatus>().PlayerDamage);
        }
    }
}
