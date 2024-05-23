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

    public override SelectTest TestClone()
    {
        return this;
    }
}
