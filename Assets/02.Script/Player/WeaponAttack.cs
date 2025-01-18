using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    private bool isAttack;
    private int playerDamage;
    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;
        playerController = Manager.Instance.Game.PlayerController;
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
        if (other.CompareTag("Enemy") && isAttack == true)
        {
            other.GetComponentInParent<EnemyStatus>().TakeDamage(SetTakePlayerDamage());
        }
    }

    private int SetTakePlayerDamage()
    {
        var status = PlayerInteractionStatus.instance;

        playerDamage = status.PlayerDamage;

        float ciriticalRange = Random.Range(0.0f, 100.0f);

        if (ciriticalRange <= status.CriticalPer)
            playerDamage += status.CriticalDamage;

        return playerDamage;
    }
}
