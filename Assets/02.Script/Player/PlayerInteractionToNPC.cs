using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractionToNPC : MonoBehaviour
{
    private bool isNPCTalk;

    private KeyCode toNPCKey;
    private void Start()
    {
        toNPCKey = Manager.Instance.Key.GetKeyCode("ToNPC");
        isNPCTalk = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(toNPCKey))
        {
            isNPCTalk = true;
            Debug.Log("Is Key Down : " + isNPCTalk);
        }

        if (Input.GetKeyUp(toNPCKey))
        {
            isNPCTalk = false;
            Debug.Log("Is Key Up : " + isNPCTalk);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC") && isNPCTalk)
        {
            NPCQuestController npc = null;
            try
            {
                npc = other.GetComponent<NPCQuestController>();
            }
            catch(NullReferenceException e)
            {
                npc = null;
                Debug.Log("This npc have not 'NPCQuestController' component " + e);
            }
            
            if(npc != null)
            {
                npc.SetNPCTalkUIValue();

                isNPCTalk = false;
            }

            Debug.Log("Is NPC ? " + npc);
        }
    }
}
