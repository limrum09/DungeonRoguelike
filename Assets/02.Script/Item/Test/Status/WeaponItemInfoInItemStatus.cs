using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameManager/ItemStatus/WeaopnItemInfo", fileName = "WeaopnItemInfo")]
public class WeaponItemInfoInItemStatus : ItemInfoInItemStatus
{
    [SerializeField]
    public WeaponItem weaponItem;
}
