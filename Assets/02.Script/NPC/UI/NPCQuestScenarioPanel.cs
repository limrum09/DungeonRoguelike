using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuestScenarioPanel : MonoBehaviour
{
    [SerializeField]
    private Scenario scenario;
    [SerializeField]
    private NPCTalkUIController parent;
    [SerializeField]
    private Image npcImage;
    [SerializeField]
    private TextMeshProUGUI npcTalkText;

    [SerializeField]
    private Button nextTextButton;
    [SerializeField]
    private Button acceptionButton;
    [SerializeField]
    private Button cancelButton;

    private int index;
    private int scenarioLength;
    private bool isQuestSuggest;


    public void GetScenario(Scenario currentScenario)
    {
        scenario = currentScenario;                     // 시나리오 
        scenarioLength = currentScenario.storys.Length; // 시나리오 전체 길이
        isQuestSuggest = currentScenario.isQuestSuggest;// 퀘스트를 수락하는 시나리오 인지 확인
        
        // 시나리오 순서 초기화
        index = 0;

        // 필요한 버튼만 보이도록 초기화
        nextTextButton.gameObject.SetActive(true);
        acceptionButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        NextScenario();
    }

    public void NextScenario()
    {
        // 시나리오 순서가 전체 시나리오의 크기보다 작을 경우 호출
        if (scenarioLength > index)
        {
            // 시나리오 진행
            npcTalkText.text = scenario.storys[index];
            index++;

            // 퀘스트를 수락 또는 거절하는 시나리오에서, 마지막 대화일 경우
            if(scenarioLength == index && isQuestSuggest)
            {
                // 다음 버튼이 안보이고, 수락과 거절 버튼이 보임
                nextTextButton.gameObject.SetActive(false);
                acceptionButton.gameObject.SetActive(true);
                cancelButton.gameObject.SetActive(true);
            }
        }
        // 시나리오 순서가 전체 시나리오 크기보다 크거나 같다는 것은 다음 시나리오가 없다는 것
        else
        {
            parent.NextQuestScenario();
        }
    }
}
