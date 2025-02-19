using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPCController : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Manager.Instance.UIAndScene.StoreUI.SetStoreState(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            Manager.Instance.UIAndScene.StoreUI.SetStoreState(false);
        }
    }
}
