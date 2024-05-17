using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player/Weapon/WeaponAnimator", fileName ="WeaponAnimator")]
public class WeaponAin : ScriptableObject
{
    public Animator animator;
    public string weaponLTag;
    public string weaponRTag;
}
