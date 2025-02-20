using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
    [SerializeField]
    ParticleSystem aura;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            if (aura != null)
            {
                aura.gameObject.SetActive(true);
                aura.Play();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            if (aura != null)
            {
                aura.Stop();
                aura.gameObject.SetActive(false);
            }
            Manager.Instance.UIAndScene.StoreUI.SetStoreState(false);
        }
    }
}
