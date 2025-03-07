using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectItem : MonoBehaviour
{
    public InvenItem item;
    [SerializeField]
    private GameObject itemMesh;
    [SerializeField]
    private ParticleSystem getItemEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            InvenData.instance.AddItem(item);
            PlayerGetPotion();
        }
    }

    protected virtual void PlayerGetPotion()
    {
        Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + 0.7f, this.transform.position.z);
        GameObject getItemParticleObject =  Instantiate(getItemEffect, spawnPos, Quaternion.identity).gameObject;
        getItemParticleObject.GetComponent<GetItemEffectController>().FindPlayer();
        

        Destroy(this.gameObject);
    }

    protected virtual void Update()
    {
        itemMesh.transform.Rotate(Vector3.forward * 40f * Time.deltaTime);
    }
}
