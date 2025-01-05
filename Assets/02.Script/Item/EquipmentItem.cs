using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ScriptableObject
{
    [Header("ItemInfo")]
    [SerializeField]
    protected string itemCode;
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected GameObject equipmentItem;
    [SerializeField]
    protected Sprite itemSprite;

    [Header("Condition")]
    [SerializeField]
    protected IsAchievementComplete condition;

    [Header("Status")]
    [SerializeField]
    protected int itemHp;
    [SerializeField]
    protected int itemDamage;
    [SerializeField]
    protected int itemCriticalDamage;
    [SerializeField]
    protected int itemSheild;
    [SerializeField]
    protected float itemCriticalPer;
    [SerializeField]
    protected float itemSpeed;
    [SerializeField]
    protected float itemCoolTime;


    public string ItemCode => itemCode;
    public string ItemName => itemName;
    public GameObject ItemObject => equipmentItem;
    public Sprite ItemSprite => itemSprite;

    public IsAchievementComplete Condition => condition;

    public int ItemHP => itemHp;
    public int ItemDamage => itemDamage;
    public int ItemCriticalDamage => itemCriticalDamage;
    public int ItemSheild => itemSheild;
    public float ItemCriticalPer => itemCriticalPer;
    public float ItemSpeed => itemSpeed;
    public float ItemCoolTime => itemCoolTime;
}
