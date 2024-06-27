using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkUIController : MonoBehaviour
{
    [SerializeField]
    private List<QuestAndScenario> questAndScenarios;

    [SerializeField]
    public QuestAndScenario selectQuest;

    [SerializeField]
    private GameObject npcBasicTalkPanel;
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

    public void QuestSelect(QuestAndScenario questAndScenario)
    {
        selectQuest = questAndScenario;

        NPCQUestScenarioTalkToPlayer(selectQuest);
    }

    public void NPCBasicScenario(Scenario basic)
    {
        npcBasicTalkPanel.SetActive(false);
        npcTalkPanel.SetActive(false);
        playerTalkPanel.SetActive(false);


    }

    public void GetQuestAndScenario(List<QuestAndScenario> questAndScenarios)
    {
        this.questAndScenarios = questAndScenarios;
        
        
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
