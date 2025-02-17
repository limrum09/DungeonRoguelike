using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quest/Reward/InvenItemReward", fileName ="InvenItemReward")]
public class InvenItemReward : Reward
{
    [SerializeField]
    private InvenItem item;

    [SerializeField]
    private int count;              // 개수

    public InvenItem Item => item;
    public int Count => count;

    public override void Give(Quest quest)
    {
        InvenItem getItem = item.Clone();

        for(int i = 0; i < count; i++)
        {
            InvenData.instance.AddItem(getItem);
        }

        Debug.Log("Get Item : " + item.name + ", Item Count : " + count);
    }
}
