using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSelect : SelectTest
{
    [SerializeField]
    private string subCategory;
    [SerializeField]
    private int indexOverlapping;

    public string SubCateogy => subCategory;
    public int IndexOverlapping => indexOverlapping;

    public void CopySelect(ArmorSelect copy)
    {
        prefab = copy.ItemPrefab;
        equipmentCategory = copy.EquipmentCategory;
        itemOverlapping = copy.ItemOverlapping;
        subCategory = copy.SubCateogy;
        indexOverlapping = copy.IndexOverlapping;
    }

    public override SelectTest TestClone()
    {
        return this;
    }
}
