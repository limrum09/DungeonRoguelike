using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPCController : NPCBasic
{
    public override void NPCTalk()
    {
        base.NPCTalk();
        Manager.Instance.UIAndScene.StoreUI.SetStoreState();
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            Manager.Instance.UIAndScene.StoreUI.SetStoreState(false);
        }
    }
}
