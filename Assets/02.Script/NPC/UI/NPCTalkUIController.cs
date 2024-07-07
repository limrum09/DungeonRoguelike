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
    private List<Scenario> currentScenario;

    [SerializeField]
    private NPCBasicTalkPanel npcBasicScenarioPanel;
    [SerializeField]
    private NPCQuestScenarioPanel npcScenarioPanel;
    [SerializeField]
    private PlayerQuestScenarioPanel playerScenarioPanel;

    private int questScenarioIndex;
    private bool isTalkUI;

    private void Start()
    {
        isTalkUI = false;
    }

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

    public void NPCBasicScenario()
    {
        npcBasicScenarioPanel.gameObject.SetActive(true);
        npcScenarioPanel.gameObject.SetActive(false);
        playerScenarioPanel.gameObject.SetActive(false);

        npcBasicScenarioPanel.NPCTalk(npcBasicTalkScenario, questAndScenarios);
    }

    public void GetQuestAndScenario(Scenario basicScenario, List<QuestAndScenario> questAndScenarios, Sprite npcImage)
    {
        if (isTalkUI)
            return;

        isTalkUI = true;

        this.questAndScenarios = questAndScenarios;
        this.npcBasicTalkScenario = basicScenario;

        questScenarioIndex = 0;

        //NPCQUestScenarioTalkToPlayer();
        NPCBasicScenario();
    }

    public void QuestSelect(QuestAndScenario questAndScenario)
    {
        Debug.Log("Quest Select !");
        selectQuest = questAndScenario;

        if(selectQuest.State == ScenarioState.Inactive)
        {
            currentScenario = selectQuest.Scenarios.NormalScenario;
        }
        else if(selectQuest.State == ScenarioState.Running)
        {
            currentScenario = selectQuest.Scenarios.RunningScenario;
        }
        else if(selectQuest.State == ScenarioState.WaitingForCompletion)
        {
            currentScenario = selectQuest.Scenarios.CompleteScenario;
        }        

        NPCQUestScenarioTalkToPlayer();
    }

    public void NextQuestScenario()
    {
        Debug.Log("Next Scenario");
        if (selectQuest != null)
        {
            NPCQUestScenarioTalkToPlayer();
        }
    }

    private void NPCQUestScenarioTalkToPlayer()
    {
        int length = currentScenario.Count;

        if (length == questScenarioIndex)
        {
            NPCTalkEnd();
            return;
        }

        if (currentScenario[questScenarioIndex].isNPCTalk)
        {
            npcScenarioPanel.gameObject.SetActive(true);
            playerScenarioPanel.gameObject.SetActive(false);

            npcScenarioPanel.GetScenario(currentScenario[questScenarioIndex]);
        }
        else
        {
            npcScenarioPanel.gameObject.SetActive(false);
            playerScenarioPanel.gameObject.SetActive(true);

            playerScenarioPanel.GetScenario(currentScenario[questScenarioIndex]);
        }

        questScenarioIndex++;
    }

    public void AccoptionQuest()
    {
        if(selectQuest != null)
        {
            questScenarioIndex = 0;
            currentScenario = selectQuest.Scenarios.AcceptionScenario;
            selectQuest.State = ScenarioState.Running;

            Quest acceptionQuest = selectQuest.Quest;

            QuestSystem.instance.QuestSystemRegister(acceptionQuest);

            NPCQUestScenarioTalkToPlayer();
        }        
    }

    public void CancelQuest()
    {
        if(selectQuest != null)
        {
            questScenarioIndex = 0;
            currentScenario = selectQuest.Scenarios.CancelScenario;

            NPCQUestScenarioTalkToPlayer();
        }
    }

    public void NPCTalkEnd()
    {
        Debug.Log("NPC TALK END");
        isTalkUI = false;
        questScenarioIndex = 0;

        npcBasicScenarioPanel.gameObject.SetActive(false);
        npcScenarioPanel.gameObject.SetActive(false);
        playerScenarioPanel.gameObject.SetActive(false);
    }
}
