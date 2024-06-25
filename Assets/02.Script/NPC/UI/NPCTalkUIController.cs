using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkUIController : MonoBehaviour
{
    [SerializeField]
    private List<Quest> quest;
    [SerializeField]
    private List<QuestScenarioCollection> scenarios;

    [SerializeField]
    private GameObject npcTalkPanel;
    [SerializeField]
    private GameObject playerTalkPanel;

    private int questScenarioIndex;

    public int QuestScenarioIndex
    {
        get
        {
            return questScenarioIndex;
        }
        set
        {
            questScenarioIndex = value;
        }
    }

    public void NPCBasicScenario(Scenario basic)
    {
        npcTalkPanel.SetActive(true);
        playerTalkPanel.SetActive(false);
    }

    public void GetQuestAndScenario(List<QuestAndScenario> questAndScenarios)
    {
        foreach(var questAndScenario in questAndScenarios)
        {
            quest.Add(questAndScenario.Quest);
            scenarios.Add(questAndScenario.Scenarios);
        }
        

        questScenarioIndex = 0;

        //NPCQUestScenarioTalkToPlayer();
    }
    private void NPCQUestScenarioTalkToPlayer(QuestAndScenario selectQuestAndScenario)
    {
        QuestScenarioCollection newCollection = selectQuestAndScenario.Scenarios;
        if (newCollection.NormalScenario[questScenarioIndex].isNPCTalk)
        {
            npcTalkPanel.SetActive(true);
            playerTalkPanel.SetActive(false);
        }
        else
        {
            npcTalkPanel.SetActive(false);
            playerTalkPanel.SetActive(true);
        }

        questScenarioIndex++;
    }
}
