using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Condition/IsQuestComplete", fileName = "IsQuestComplete_")]
public class IsQuestComplete : QuestCondition
{
    [SerializeField]
    private Quest target;
    public override bool IsPass(Quest quest)
    {
        return QuestSystem.instance.ContainsCompletedQuest(target);
    }
}
