using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem aura;

    public virtual void NPCTalk()
    {
        Manager.Instance.canInputKey = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
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
    protected virtual void OnTriggerStay(Collider other)
    {
        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            Manager.Instance.canInputKey = true;
            if (aura != null)
            {
                aura.Stop();
                aura.gameObject.SetActive(false);
            }
        }
    }
}
