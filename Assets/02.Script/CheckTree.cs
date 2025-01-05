using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DDE " + other.name);
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Enter 나무 : " + other.name);
        }

        if (other.CompareTag("Wand"))
        {
            Debug.Log("Enter " + other.name);
        }
        if (other.CompareTag("NPC"))
        {
            Debug.Log("Enter NPC : " + other.name);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Stay 나무 : " + other.name);
        }

        if (other.CompareTag("Wand"))
        {
            Debug.Log("Stay " + other.name);
        }
        if (other.CompareTag("NPC"))
        {
            Debug.Log("Stay NPC : " + other.name);
        }
    }
}
