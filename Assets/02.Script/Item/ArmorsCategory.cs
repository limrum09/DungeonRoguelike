using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorsCategory : MonoBehaviour
{
    [SerializeField]
    private EquipmentCategory armorCategory;
    [SerializeField]
    private string subCategory;
    [SerializeField]
    public MeshFilter itemMeshFilter;
    [SerializeField]
    private int indexOverLapping;
    [SerializeField]
    public bool overLapping;

    public int IndexOverLapping => indexOverLapping;

    public EquipmentCategory ArmorCategory => armorCategory;
    public string SubCategory => subCategory;

    public void ChangeFilter(MeshFilter changeFilter)
    {
        if (itemMeshFilter == null)
            return;

        if(changeFilter == null)
            itemMeshFilter.sharedMesh = null;
        else
            itemMeshFilter.sharedMesh = changeFilter.sharedMesh;
    }
}
