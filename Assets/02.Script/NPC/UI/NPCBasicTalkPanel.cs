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

    // NPC마다 퀘스트를 가지고 있기에, NPC와 대화하면 중복 여부를 따지지 않고 무조건 모조리 지운다.
    // 이후, 퀘스트 선택지를 다시 만든다.
    private void QuestListInitialized()
    {
        int questCount = content.childCount;

        for(int i = questCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void NPCTalk(Scenario basicScenario, List<QuestAndScenario> questAndScenarios)
    {
        // 초기화
        QuestListInitialized();

        // NPC가 가지고 있는 퀘스트시나리오의 숫자
        int questCount = questAndScenarios.Count;

        // Scroll View의 Content의 크기를 개수 만큼 일정하게 정한다.
        content.sizeDelta = new Vector2(content.sizeDelta.x, 5.0f + (55.0f * questCount));

        // 기본 대화는 한페이지로만 설정함, 여러 페이지 사용 시 수정 필요
        basicText.text = basicScenario.storys[0];

        // NPC가 가지고있는 퀘스트 만큼 선택지를 만든다.
        foreach(var questAndScenario in questAndScenarios)
        {
            Debug.Log("Quest And Scenario : " + questAndScenario);
            NPCSelectQuest newSelectQuest = Instantiate(npcSelectQuestPrefab, content);
            newSelectQuest.GetQuestAndScenario(questAndScenario);
        }
    }
}
