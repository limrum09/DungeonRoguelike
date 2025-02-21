using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCSelectQuest : MonoBehaviour
{
    [SerializeField]
    private QuestAndScenario questAndScenario;
    [SerializeField]
    private TextMeshProUGUI questTitle;

    [SerializeField]
    private Image stateImage;
    [SerializeField]
    private Sprite[] questStateImages;

    private Quest thisQuest;
    private Quest activeQuest;
    private ScenarioState scenarioState;

    private void Start()
    {
        activeQuest = null;
    }

    public void GetQuestAndScenario(QuestAndScenario getQuestAndScenario)
    {
        questAndScenario = getQuestAndScenario;

        thisQuest = getQuestAndScenario.Quest;
        
        // 이미 완료 된 퀘스트는 제거한다.
        if(thisQuest.State == QuestState.Complete || !thisQuest.IsAcceptable)
        {
            Destroy(this.gameObject);
        }

        questTitle.text = thisQuest.DisplayName;

        // QuestSystem에서 현제 퀘스트가 activeQuest인지 확인하기
        activeQuest = Manager.Instance.Quest.GetActiveQuest(thisQuest);

        if(activeQuest != null)
        {
            if (activeQuest.State == QuestState.WaitingForCompletion)
                getQuestAndScenario.State = ScenarioState.WaitingForCompletion;
        }

        scenarioState = getQuestAndScenario.State;

        // 현제 퀘스트의 상태에 따라 이미지가 달라진다.
        switch (scenarioState)
        {
            case ScenarioState.Inactive:
                stateImage.sprite = questStateImages[0];
                break;
            case ScenarioState.Running:
                stateImage.sprite = questStateImages[1];
                break;
            case ScenarioState.WaitingForCompletion:
                stateImage.sprite = questStateImages[2];
                break;
        }
    }

    // 퀘스트 선택 시, 호출. 퀘스트 선택 Object는 Button이 들어가 있어 본인을 호출한다.
    public void SelectThisQuest()
    {
        NPCTalkUIController root = FindObjectOfType<NPCTalkUIController>();

        root.QuestSelect(questAndScenario);

        // 퀘스트 상태가 'WaitingForCompeltion'일 경우, 퀘스트가 완료가됨.
        if(activeQuest != null)
        {
            if (activeQuest.State == QuestState.WaitingForCompletion)
            {
                Manager.Instance.Quest.CompletedWaitingQuest(activeQuest);
            }
        }
    }
}
