using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListContainer : MonoBehaviour
{
    [SerializeField]
    private Quest quest;
    [SerializeField]
    private TextMeshProUGUI questTitle;
    [SerializeField]
    private QuestListCheckTrackerViewButton checkBoxButton;

    public void UpdateQuestList(Quest inputQuest)
    {
        quest = inputQuest;
        if(checkBoxButton != null)
            checkBoxButton.SetQuest(quest);

        questTitle.text = quest.DisplayName;
    }

    public void ShowDetailQuestView()
    {
        Manager.Instance.UIAndScene.ChangeQuestDetailView(quest);
    }
}
