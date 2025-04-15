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

    public void NPCTalkControllerStart()
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

    public void CloseQuestUI()
    {
        npcBasicScenarioPanel.gameObject.SetActive(false);
        npcScenarioPanel.gameObject.SetActive(false);
        playerScenarioPanel.gameObject.SetActive(false);

        isTalkUI = false;
    }

    public void NPCBasicScenario()
    {
        npcBasicScenarioPanel.gameObject.SetActive(true);   // 기본적으로 NPC가 말하는 UI
        npcScenarioPanel.gameObject.SetActive(false);       // Quest 선택 후, NPC의 대화상자
        playerScenarioPanel.gameObject.SetActive(false);    // Quest 선택 후, Player의 대화상자

        // 기본 NPC 대화상자 호출
        npcBasicScenarioPanel.NPCTalk(npcBasicTalkScenario, questAndScenarios, this);
    }

    // 호출 시 퀘스트UI 시작
    public void GetQuestAndScenario(Scenario basicScenario, List<QuestAndScenario> questAndScenarios, Sprite npcImage)
    {
        if (isTalkUI)
            return;

        isTalkUI = true;

        this.questAndScenarios = questAndScenarios;
        this.npcBasicTalkScenario = basicScenario;

        questScenarioIndex = 0;

        NPCBasicScenario();
    }

    public void QuestSelect(QuestAndScenario questAndScenario)
    {
        Debug.Log("Quest Select !");
        selectQuest = questAndScenario;

        // 퀘스트 선택 시, 현제 상태에 따라 시나리오가 정해짐
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

        NPCQuestScenarioTalkToPlayer();
    }

    // Player나 NPC가 본인의 Scenario가 끝났을 시, 호출
    public void NextQuestScenario()
    {
        Debug.Log("Next Scenario");
        if (selectQuest != null)
        {
            NPCQuestScenarioTalkToPlayer();
        }
    }

    // 시나리오 재생
    private void NPCQuestScenarioTalkToPlayer()
    {
        int length = currentScenario.Count;

        // 준비된 시나리오가 없는 경우
        if (length == questScenarioIndex)
        {
            // 종료
            NPCTalkEnd();
            return;
        }

        // NPC 시나리오
        if (currentScenario[questScenarioIndex].isNPCTalk)
        {
            npcScenarioPanel.gameObject.SetActive(true);
            playerScenarioPanel.gameObject.SetActive(false);

            npcScenarioPanel.GetScenario(currentScenario[questScenarioIndex]);
        }
        // Player 사나리오
        else
        {
            npcScenarioPanel.gameObject.SetActive(false);
            playerScenarioPanel.gameObject.SetActive(true);

            playerScenarioPanel.GetScenario(currentScenario[questScenarioIndex]);
        }

        questScenarioIndex++;
    }

    // 퀘스트 수락 시
    public void AccoptionQuest()
    {
        if(selectQuest != null)
        {
            questScenarioIndex = 0;                                     // 시나리오 순서 초기화
            currentScenario = selectQuest.Scenarios.AcceptionScenario;  // 현제 시나리오를 퀘스트 수락 시나리오로 변경
            selectQuest.State = ScenarioState.Running;                  // 시나리오 상태 변경

            Manager.Instance.Quest.QuestSystemRegister(selectQuest.Quest);   // QuestSystem에 퀘스트를 시작하도록 추가
            selectQuest.RegisterQuestCompletedEvent();

            NPCQuestScenarioTalkToPlayer();
        }        
    }

    // 거절 시
    public void CancelQuest()
    {
        if(selectQuest != null)
        {
            questScenarioIndex = 0;                                 // 시나리오 순서 초기화
            currentScenario = selectQuest.Scenarios.CancelScenario; // 현제 시나리오를 퀘스트 거절 시나리오로 변경

            NPCQuestScenarioTalkToPlayer();
        }
    }

    // 시나리오 종료
    public void NPCTalkEnd()
    {
        isTalkUI = false;
        questScenarioIndex = 0;

        npcBasicScenarioPanel.gameObject.SetActive(false);
        npcScenarioPanel.gameObject.SetActive(false);
        playerScenarioPanel.gameObject.SetActive(false);
    }
}
