using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="Armor_", menuName ="Scriptable Object/ArmorItem")]
public class ArmorItem : EquipmentItem
{
    [Header("Option")]
    [SerializeField]
    private EquipmentCategory equipmentCategory;
    [SerializeField]
    private string subCategory;
    [SerializeField]
    private bool itemOverlapping;
    [SerializeField]
    private int indexOverlapping;

    public bool HaveItem => ItemObject != null;

    public EquipmentCategory EquipmentCategory => equipmentCategory;
    public string SubCategory => subCategory;
    public bool ItemOverlapping => itemOverlapping;
    public int IndexOverlapping => indexOverlapping;
}
