using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkUIController : MonoBehaviour
{
    [SerializeField]
    private Scenario npcBasicTalkScenario;
    [SerializeField]
    private List<QuestAndScenario> questAndScenarios;
    

    [SerializeField]
    public QuestAndScenario selectQuest;

    [SerializeField]
    private NPCBasicTalkPanel npcBasicTalkPanel;
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
        Debug.Log("Quest Select !");
        selectQuest = questAndScenario;

        NPCQUestScenarioTalkToPlayer(selectQuest);
    }

    public void NPCBasicScenario()
    {
        npcBasicTalkPanel.gameObject.SetActive(true);
        npcTalkPanel.SetActive(false);
        playerTalkPanel.SetActive(false);

        npcBasicTalkPanel.NPCTalk(npcBasicTalkScenario, questAndScenarios);
    }

    public void GetQuestAndScenario(Scenario basicScenario, List<QuestAndScenario> questAndScenarios)
    {
        this.questAndScenarios = questAndScenarios;
        this.npcBasicTalkScenario = basicScenario;
        
        questScenarioIndex = 0;

        //NPCQUestScenarioTalkToPlayer();
        NPCBasicScenario();
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
