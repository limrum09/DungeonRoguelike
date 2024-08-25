using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Armor_", menuName ="Scriptable Object/ArmorItem")]
public class ArmorItem : ScriptableObject
{
    [Header("Objects")]
    [SerializeField]
    private string itemCode;
    [SerializeField]
    private GameObject armorItem;
    [SerializeField]
    private Sprite armorItemSprite;

    [Header("Option")]
    [SerializeField]
    private EquipmentCategory equipmentCategory;
    [SerializeField]
    private string subCategory;
    [SerializeField]
    private bool itemOverlapping;
    [SerializeField]
    private int indexOverlapping;

    [Header("Status")]
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


    public string ItemCode => itemCode;
    public GameObject ArmorItemObject => armorItem;
    public Sprite ArmorItemSprite => armorItemSprite;
    public bool HaveItem => armorItem != null;

    public EquipmentCategory EquipmentCategory => equipmentCategory;
    public string SubCategory => subCategory;
    public bool ItemOverlapping => itemOverlapping;
    public int IndexOverlapping => indexOverlapping;

    public int ItemHP => itemHp;
    public int ItemDamage => itemDamage;
    public int ItemCriticalDamage => itemCriticalDamage;
    public int ItemSheild => itemSheild;
    public float ItemCriticalPer => itemCriticalPer;
    public float ItemSpeed => itemSpeed;
    public float ItemCoolTime => itemCoolTime;
}
