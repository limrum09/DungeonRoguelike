using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Reward/EquipmentAchievementReward", fileName = "EquipmentAchievementReward_")]
public class EquipmentAchievementReward : Reward
{
    [SerializeField]
    private EquipmentItem item;

    public string UnLockItemName => item.ItemName;

    public override void Give(Quest quest)
    {
        
    }
}
