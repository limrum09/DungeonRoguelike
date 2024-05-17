using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActive : MonoBehaviour
{
    [SerializeField]
    private int itemHp;
    [SerializeField]
    private int itemDamage;
    [SerializeField]
    private int itemCriticalDamage;
    [SerializeField]
    private int itemSheild;
    [SerializeField]
    private float itemCriticalPer;
    [SerializeField]
    private float itemSpeed;
    [SerializeField]
    private float itemCoolTime;

    private void OnEnable()
    {
        if (ItemStatus.instance != null)
        {
            ItemStatusAdd();
        }
        
    }
    private void OnDisable()
    {
        if (ItemStatus.instance != null)
        {
            ItemStatusMinus();
        }
    }

    private void ItemStatusAdd()
    {
        Debug.Log("AddStatus " + this.gameObject.name);
        ItemStatus.instance.ItemHP += itemHp;
        ItemStatus.instance.ItemDamage += itemDamage;
        ItemStatus.instance.ItemCriticalDamage += itemCriticalDamage;
        ItemStatus.instance.ItemSheid += itemSheild;
        ItemStatus.instance.ItemCriticalPer += itemCriticalPer;
        ItemStatus.instance.ItemSpeed += itemSpeed;
        ItemStatus.instance.ItemCoolTime += itemCoolTime;
        ItemStatus.instance.PlayerAddItemStatus();
    }

    private void ItemStatusMinus()
    {
        Debug.Log("LostStatus " + this.gameObject.name);
        ItemStatus.instance.ItemHP -= itemHp;
        ItemStatus.instance.ItemDamage -= itemDamage;
        ItemStatus.instance.ItemCriticalDamage -= itemCriticalDamage;
        ItemStatus.instance.ItemSheid -= itemSheild;
        ItemStatus.instance.ItemCriticalPer -= itemCriticalPer;
        ItemStatus.instance.ItemSpeed -= itemSpeed;
        ItemStatus.instance.ItemCoolTime -= itemCoolTime;
        ItemStatus.instance.PlayerLostItemStatus();
    }
}
