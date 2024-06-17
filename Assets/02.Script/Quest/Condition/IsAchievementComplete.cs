using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Condition/IsAchievementComplete", fileName = "IsAchievementComplete_")]
public class IsAchievementComplete : QuestCondition
{
    [SerializeField]
    private Quest target;
    public override bool IsPass(Quest quest)
    {
        return QuestSystem.instance.ContainsCompletedAchievements(target);
    }
}
