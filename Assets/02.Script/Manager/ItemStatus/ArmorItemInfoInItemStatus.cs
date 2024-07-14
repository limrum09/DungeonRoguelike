using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameManager/ItemStatus/ArmorItemInfo", fileName = "ArmorItemInfo_")]
public class ArmorItemInfoInItemStatus : ItemInfoInItemStatus
{
    [SerializeField]
    private string subCategory;
    [SerializeField]
    public ArmorItem armorItem;

    public string SubCategory => subCategory;
}
