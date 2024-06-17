using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private QuestListContentController activeQuestView;
    [SerializeField]
    private QuestListContentController completeQuestView;


    public void ListViewStart()
    {
        var questSystem = QuestSystem.instance;

        foreach(var quest in questSystem.ActiveQeusts)
        {
            AddQuestToActiveQuestListView(quest);
        }

        foreach(var quest in questSystem.CompletedQuests)
        {
            AddQuestToCompleteQuestListView(quest);
        }

        questSystem.onQuestRegisterd += AddQuestToActiveQuestListView;

        questSystem.onQuestCompleted += RemoveQuestToActiveQuestToListView;
        questSystem.onQuestCompleted += AddQuestToCompleteQuestListView;
        
        
    }

    private void OnDestroy()
    {
        var questSystem = QuestSystem.instance;

        if (questSystem)
        {
            questSystem.onQuestRegisterd -= AddQuestToActiveQuestListView;

            questSystem.onQuestCompleted -= RemoveQuestToActiveQuestToListView;
            questSystem.onQuestCompleted -= AddQuestToCompleteQuestListView;
        }
    }

    private void AddQuestToActiveQuestListView(Quest quest)
    {
        activeQuestView.AddElement(quest);
    }

    private void AddQuestToCompleteQuestListView(Quest quest)
    {
        completeQuestView.AddElement(quest);
    }

    private void RemoveQuestToActiveQuestToListView(Quest quest)
    {
        activeQuestView.RemoveElement(quest);
    }
}
