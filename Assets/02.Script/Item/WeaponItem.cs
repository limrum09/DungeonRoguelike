using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Object/WaeponItem")]
public class WeaponItem : ScriptableObject
{
    [Header("Objects")]
    [SerializeField]
    private string itemCode;
    [SerializeField]
    private GameObject weaponItem;
    [SerializeField]
    private Sprite weaponItemSprite;

    [Header("Option")]
    [SerializeField]
    private EquipmentCategory equipmentCategory;
    [SerializeField]
    private bool useOneHand;
    [SerializeField]
    private bool leftWeapon;
    [SerializeField]
    private int weaponValue;

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
    public GameObject WeaponItemObject => weaponItem;
    public Sprite WeaponItemSprite => weaponItemSprite;
    public EquipmentCategory EquipmentCategory => equipmentCategory;
    public bool UseOndeHand => useOneHand;
    public bool LeftWeapon => leftWeapon;
    public int WeaponValue => weaponValue;

    public int ItemHP => itemHp;
    public int ItemDamage => itemDamage;
    public int ItemCriticalDamage => itemCriticalDamage;
    public int ItemSheild => itemSheild;
    public float ItemCriticalPer => itemCriticalPer;
    public float ItemSpeed => itemSpeed;
    public float ItemCoolTime => itemCoolTime;
}