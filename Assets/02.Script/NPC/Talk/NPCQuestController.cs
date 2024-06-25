using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestController : MonoBehaviour
{
    [SerializeField]
    private Sprite npcImage;
    [SerializeField]
    private Scenario basicScenario;
    [SerializeField]
    private List<QuestAndScenario> questAndScenario;


    public Scenario BasicScenario => basicScenario;
    public List<QuestAndScenario> QuestAndScenario => questAndScenario;
    public Sprite NPCImage => npcImage;
}
