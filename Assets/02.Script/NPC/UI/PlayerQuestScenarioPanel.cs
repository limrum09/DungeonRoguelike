using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerQuestScenarioPanel : MonoBehaviour
{
    [SerializeField]
    private Scenario scenario;
    [SerializeField]
    private NPCTalkUIController parent;
    [SerializeField]
    private TextMeshProUGUI npcTalkText;

    private int index;
    private int scenarioLength;


    public void GetScenario(Scenario currentScenario)
    {
        scenario = currentScenario;                     // 현제 시작하는 시나리오
        scenarioLength = currentScenario.storys.Length; // 시나리오의 전체 길이

        index = 0;  // 시나리오 순서 초기화

        NextScenario();
    }

    // 다음 시나리오 재생
    public void NextScenario()
    {
        // 현제 시나리오 순서가 전체 시나리오보다 작을 경우 
        if (scenarioLength > index)
        {
            // 시나리오 재생
            npcTalkText.text = scenario.storys[index];
            index++;
        }
        // 시나리오의 순서가 전체 시나리오의 크기보다 크거나 같다는 것은 다름 시나리오가 없다는 뜻이다.
        else
        {
            parent.NextQuestScenario();
        }
    }
}
