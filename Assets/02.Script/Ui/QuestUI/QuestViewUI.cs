using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestViewUI : MonoBehaviour
{
    [Header("Quest UI")]
    [SerializeField]
    private GameObject questViewUI;

    [Header("List View")]
    [SerializeField]
    private QuestListViewController listView;
    [SerializeField]
    private QuestListToggle activeQuestToggle;
    [SerializeField]
    private QuestListToggle completedQuestToggle;

    [Header("Detail View")]
    [SerializeField]
    private QuestDetailViewController detailView;

    [Header("Tracker View")]
    [SerializeField]
    private QuestTrackerView trackerView;
    

    public void QuestUIStart()
    {
        listView.ListViewStart();
        detailView.DetailViewStart();
        trackerView.TrackerViewStart();

        activeQuestToggle.SelectToggle();
        completedQuestToggle.SelectToggle();

        var enableList = listView.Content.transform.GetChild(0);

        if(enableList != null)
        {
            var questListContainer = enableList.GetComponent<QuestListContainer>();
            questListContainer.ShowDetailQuestView();
        }

        questViewUI.SetActive(false);
    }

    public void SetTrackerViewQuest(Quest quest, bool isOn)
    {
        if (isOn)
            trackerView.QuestInputToTrackerView(quest);
        else
            trackerView.QuestRemoveToTrackerView(quest);
    }
}
