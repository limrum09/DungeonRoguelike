using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectItem : MonoBehaviour
{
    public InvenItem item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            InvenData.instance.CheckItem(item);
        }
    }
}
