using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractionToNPC : MonoBehaviour
{
    private bool isNPCTalk;

    private void Start()
    {
        isNPCTalk = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(Manager.Instance.Key.GetKeyCode("ToNPC")))
        {
            isNPCTalk = true;
        }

        if (Input.GetKeyUp(Manager.Instance.Key.GetKeyCode("ToNPC")))
        {
            isNPCTalk = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC") && isNPCTalk)
        {
            NPCBasic npc = null;
            try
            {
                npc = other.GetComponent<NPCBasic>();
            }
            catch(NullReferenceException e)
            {
                npc = null;
                Debug.LogWarning("This npc have not 'NPCQuestController' component " + e);
            }
            
            if(npc != null)
            {
                npc.NPCTalk();

                isNPCTalk = false;
            }
        }
    }
}
