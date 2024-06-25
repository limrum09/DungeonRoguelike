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
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            isNPCTalk = false;
        }
    }
    private void OnTriggerEnter(Collider other)
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
                Sprite npcImage = npc.NPCImage;
                Scenario basicScenario = npc.BasicScenario;
                List<QuestAndScenario> newQuestAndScenario = npc.QuestAndScenario;

                // UNSceneManager에게 넘겨주기
            }            
        }
    }
}
