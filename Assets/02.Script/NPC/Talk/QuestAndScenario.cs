using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenarioState
{
    Inactive,
    Running,
    WaitingForCompletion
}

[CreateAssetMenu(menuName = "Quest/Story/QuestAndScenario", fileName = "QuestAndScenario")]
public class QuestAndScenario : ScriptableObject
{
    [SerializeField]
    private ScenarioState state;
    [SerializeField]
    private QuestScenarioCollection scenarios;
    [SerializeField]
    private Quest quest;

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
}
