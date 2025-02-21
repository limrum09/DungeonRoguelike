using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestController : NPCBasic
{
    [SerializeField]
    private Sprite npcImage;
    [SerializeField]
    private Scenario basicScenario;
    [SerializeField]
    private List<QuestAndScenario> questAndScenario;

    public override void NPCTalk()
    {
        base.NPCTalk();
        Manager.Instance.UIAndScene.NPCQuestTalkWithPlayer(basicScenario, questAndScenario, npcImage);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            Manager.Instance.UIAndScene.PlayerInQuestNPC();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            Manager.Instance.UIAndScene.PlayerOutQuestNPC();
        }
    }
}
