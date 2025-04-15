using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenarioState
{
    Inactive,
    Running,
    WaitingForCompletion,
    Completed
}

[CreateAssetMenu(menuName = "Quest/Story/QuestAndScenario", fileName = "QuestAndScenario")]
public class QuestAndScenario : ScriptableObject
{
    [SerializeField]
    private bool loop;
    [SerializeField]
    private ScenarioState state;
    [SerializeField]
    private QuestScenarioCollection scenarios;
    [SerializeField]
    private Quest quest;

    public bool IsLoop => loop;
    public ScenarioState State { 
        set
        {
            state = value;
        }
        get
        {
            return state;
        }
    }

    public QuestScenarioCollection Scenarios => scenarios;
    public Quest Quest => quest;

    public void RegisterQuestCompletedEvent()
    {
        quest.onCompleted += QuestCompleted;
    }
    public void UnRegisterQuestCompletedEvent()
    {
        quest.onCompleted -= QuestCompleted;
    }

    public void QuestCompleted(Quest quest)
    {
        state = loop ? ScenarioState.Inactive : ScenarioState.Completed;

        UnRegisterQuestCompletedEvent();
    }
}
