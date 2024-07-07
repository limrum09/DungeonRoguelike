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
        
        if(thisQuest.State == QuestState.Complete || !thisQuest.IsAcceptable)
        {
            Destroy(this.gameObject);
        }

        questTitle.text = thisQuest.DisplayName;

        activeQuest = QuestSystem.instance.GetActiveQuest(thisQuest);

        if(activeQuest != null)
        {
            if (activeQuest.State == QuestState.WaitingForCompletion)
                getQuestAndScenario.State = ScenarioState.WaitingForCompletion;
        }

        scenarioState = getQuestAndScenario.State;

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

    public void SelectThisQuest()
    {
        NPCTalkUIController root = FindObjectOfType<NPCTalkUIController>();

        root.QuestSelect(questAndScenario);

        // if Quest's State is WaitingForCompeltion then Quest Complete
        if(activeQuest != null)
        {
            if (activeQuest.State == QuestState.WaitingForCompletion)
            {
                QuestSystem.instance.CompletedWaitingQuest(activeQuest);
            }                
        }
    }
}
