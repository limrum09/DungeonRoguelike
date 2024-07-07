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
        scenario = currentScenario;
        scenarioLength = currentScenario.storys.Length;

        index = 0;

        NextScenario();
    }

    public void NextScenario()
    {
        if (scenarioLength > index)
        {
            npcTalkText.text = scenario.storys[index];
            index++;
        }
        else
        {
            parent.NextQuestScenario();
        }
    }
}
