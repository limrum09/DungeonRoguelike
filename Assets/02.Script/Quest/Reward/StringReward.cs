using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Reward/StringReward", fileName = "StringReward_")]
public class StringReward : Reward
{
    public string unLockItemCode;
    public override void Give(Quest quest)
    {
        
    }
}
