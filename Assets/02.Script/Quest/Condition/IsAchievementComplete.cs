using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Condition/IsAchievementComplete", fileName = "IsAchievementComplete_")]
public class IsAchievementComplete : QuestCondition
{
    [SerializeField]
    private Quest target;

    public string TargetName => target.DisplayName;
    public bool IsAchievementPass => IsPass(target);
    public override bool IsPass(Quest quest)
    {
        return Manager.Instance.Quest.ContainsCompletedAchievements(target);
    }
}
