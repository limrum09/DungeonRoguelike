using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private PlayerInteractionTest weapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            player.StartSwim();
            weapon.StartSwim();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            player.EndSwim();
            weapon.EndSwim();
        }
    }
}
