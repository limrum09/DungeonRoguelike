using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Object/WaeponItem")]
public class WeaponItem : EquipmentItem
{
    [Header("Option")]
    [SerializeField]
    private EquipmentCategory equipmentCategory;
    [SerializeField]
    private bool useOneHand;
    [SerializeField]
    private bool leftWeapon;
    [SerializeField]
    private int weaponValue;

    public EquipmentCategory EquipmentCategory => equipmentCategory;
    public bool UseOndeHand => useOneHand;
    public bool LeftWeapon => leftWeapon;
    public int WeaponValue => weaponValue;
}