using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPCController : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {

        }
    }
}
