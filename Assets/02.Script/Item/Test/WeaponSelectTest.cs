using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectTest : SelectTest
{
    [SerializeField]
    private bool useOneHand;
    [SerializeField]
    private Animator weaponAnimator;

    public Animator WeaponAnimator => weaponAnimator;

    public override SelectTest TestClone()
    {
        return this;
    }
}
