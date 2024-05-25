using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameManager/ItemStatus/ItemInfo", fileName ="ItemInfo_")]
public class ItemInfoInItemStatus : ScriptableObject
{
    [SerializeField]
    private EquipmentCategory itemCategory;
    [SerializeField]
    public ItemPart itemPart;

    public EquipmentCategory ItemCategory => itemCategory;
    public ItemPart SelectItemPart => itemPart;
}
