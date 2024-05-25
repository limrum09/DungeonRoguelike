using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class WeaponSelect : SelectTest
{
    [SerializeField]
    private bool useOneHand;
    [SerializeField]
    private bool leftWeapon;
    [SerializeField]
    private AnimatorController weaponAnimator;


    public bool UseOndeHand => useOneHand;
    public bool LeftWeapon => leftWeapon;
    public AnimatorController WeaponAnimator => weaponAnimator;


    public void CopySelect(WeaponSelect copy)
    {
        prefab = copy.ItemPrefab;
        equipmentCategory = copy.EquipmentCategory;
        itemOverlapping = copy.ItemOverlapping;
        useOneHand = copy.UseOndeHand;
        leftWeapon = copy.LeftWeapon;
        weaponAnimator = copy.WeaponAnimator;
    }

    public override SelectTest TestClone()
    {
        return this;
    }
}
