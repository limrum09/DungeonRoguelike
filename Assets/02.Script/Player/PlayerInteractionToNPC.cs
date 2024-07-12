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
        if (Input.GetKeyDown(KeyCode.X))
        {
            isNPCTalk = true;
            Debug.Log("Is Key Down : " + isNPCTalk);
        }

        if (Input.GetKeyUp(KeyCode.X))
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
                Debug.Log("This npc have not 'NPCQuestController' component");
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
