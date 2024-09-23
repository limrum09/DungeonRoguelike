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

    public void SetNPCTalkUIValue()
    {
        Manager.Instance.UIAndScene.NPCQuestTalkWithPlayer(basicScenario, questAndScenario, npcImage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Manager.Instance.UIAndScene.PlayerInQuestNPC();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Manager.Instance.UIAndScene.PlayerOutQuestNPC();
        }
    }
}
