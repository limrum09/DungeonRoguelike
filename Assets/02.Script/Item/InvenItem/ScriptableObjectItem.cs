using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectItem : MonoBehaviour
{
    public InvenItem item;
    [SerializeField]
    private GameObject itemMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            InvenData.instance.AddItem(item);
            PlayerGetPotion();
        }
    }

    protected virtual void PlayerGetPotion() {
        Destroy(this.gameObject);
    }

    protected virtual void Update()
    {
        itemMesh.transform.Rotate(Vector3.forward * 40f * Time.deltaTime);
    }
}
