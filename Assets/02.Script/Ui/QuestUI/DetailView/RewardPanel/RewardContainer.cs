using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardContainer : MonoBehaviour
{
    [SerializeField]
    private InvenItem getItem;
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private TextMeshProUGUI itemCount;
    
    public void AddItemReward(InvenItemReward item)
    {
        getItem = item.Item;

        itemIcon.sprite = item.Icon;

        itemCount.text = item.Count.ToString();
    }
}
