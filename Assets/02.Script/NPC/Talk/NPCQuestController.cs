using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestController : MonoBehaviour
{
    [SerializeField]
    private Sprite npcImage;
    [SerializeField]
    private Scenario basicScenario;
    [SerializeField]
    private List<QuestAndScenario> questAndScenario;


    public Scenario BasicScenario => basicScenario;
    public List<QuestAndScenario> QuestAndScenario => questAndScenario;
    public Sprite NPCImage => npcImage;

    public void SetNPCTalkUIValue()
    {
        UIAndSceneManager.instance.NPCQuestTalkWithPlayer(basicScenario, questAndScenario, npcImage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIAndSceneManager.instance.PlayerInQuestNPC();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIAndSceneManager.instance.PlayerOutQuestNPC();
        }
    }
}
