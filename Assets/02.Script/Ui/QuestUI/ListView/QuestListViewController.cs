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

    public GameObject Content => content;
    public void ListViewStart()
    {
        var questSystem = Manager.Instance.Quest;

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
        var questSystem = Manager.Instance.Quest;

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
