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
        scenario = currentScenario;
        scenarioLength = currentScenario.storys.Length;
        isQuestSuggest = currentScenario.isQuestSuggest;
        
        index = 0;
        nextTextButton.gameObject.SetActive(true);
        acceptionButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);        

        NextScenario();
    }

    public void NextScenario()
    {
        if (scenarioLength > index)
        {
            npcTalkText.text = scenario.storys[index];
            index++;

            if(scenarioLength == index && isQuestSuggest)
            {
                nextTextButton.gameObject.SetActive(false);
                acceptionButton.gameObject.SetActive(true);
                cancelButton.gameObject.SetActive(true);
            }
        }
        else
        {
            parent.NextQuestScenario();
        }
    }
}
