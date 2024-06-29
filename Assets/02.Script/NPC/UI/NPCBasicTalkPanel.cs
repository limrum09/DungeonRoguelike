using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCBasicTalkPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI basicText;

    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private NPCSelectQuest npcSelectQuestPrefab;

    public void NPCTalk(Scenario basicScenario, List<QuestAndScenario> questAndScenarios)
    {
        int questCount = questAndScenarios.Count;

        content.sizeDelta = new Vector2(content.sizeDelta.x, 5.0f + (55.0f * questCount));

        basicText.text = basicScenario.storys[0];

        foreach(var questAndScenario in questAndScenarios)
        {
            NPCSelectQuest newSelectQuest = Instantiate(npcSelectQuestPrefab, content);
            newSelectQuest.GetQuestAndScenario(questAndScenario);
        }
    }
}
