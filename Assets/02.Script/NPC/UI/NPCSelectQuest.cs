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

    public void GetQuestAndScenario(QuestAndScenario getQuestAndScenario)
    {
        questAndScenario = getQuestAndScenario;

        Quest getQuest = getQuestAndScenario.Quest;

        if(getQuest.State == QuestState.Complete)
        {
            Destroy(this.gameObject);
        }

        questTitle.text = getQuest.DisplayName;

        switch (getQuest.State)
        {
            case QuestState.Inactive:
                stateImage.sprite = questStateImages[0];
                break;
            case QuestState.Running:
                stateImage.sprite = questStateImages[1];
                break;
            case QuestState.WaitingForCompletion:
                stateImage.sprite = questStateImages[2];
                break;
        }
    }

    public void SelectThisQuest()
    {
        NPCTalkUIController root = FindObjectOfType<NPCTalkUIController>();

        root.QuestSelect(questAndScenario);
    }
}
