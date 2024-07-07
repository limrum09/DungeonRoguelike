using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Story/Collection", fileName ="Collection")]
public class QuestScenarioCollection : ScriptableObject
{
    [SerializeField]
    private List<Scenario> normalStory;
    [SerializeField]
    private List<Scenario> acceptionStory;
    [SerializeField]
    private List<Scenario> cancelStory;
    [SerializeField]
    private List<Scenario> runningScenario;
    [SerializeField]
    private List<Scenario> completeScenario;

    public List<Scenario> NormalScenario => normalStory;
    public List<Scenario> AcceptionScenario => acceptionStory;
    public List<Scenario> CancelScenario => cancelStory;
    public List<Scenario> RunningScenario => runningScenario;
    public List<Scenario> CompleteScenario => completeScenario;
}
