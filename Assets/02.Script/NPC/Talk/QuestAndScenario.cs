using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Story/QuestAndScenario", fileName = "QuestAndScenario")]
public class QuestAndScenario : ScriptableObject
{
    [SerializeField]
    private QuestScenarioCollection scenarios;
    [SerializeField]
    private Quest quest;

    public QuestScenarioCollection Scenarios => scenarios;
    public Quest Quest => quest;
}
