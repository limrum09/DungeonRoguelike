using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSelectQuest : MonoBehaviour
{
    [SerializeField]
    private QuestAndScenario questAndScenario;
    private Image currentStateImage;
    [SerializeField]
    private Image[] questStateImages;

    public void GetQuestAndScenario(QuestAndScenario getQuestAndScenario)
    {
        questAndScenario = getQuestAndScenario;
    }

    public void SelectThisQuest()
    {
        NPCTalkUIController root = FindObjectOfType<NPCTalkUIController>();

        root.QuestSelect(questAndScenario);
    }
}
