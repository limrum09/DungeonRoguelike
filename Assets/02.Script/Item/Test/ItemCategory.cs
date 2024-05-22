using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCategory : MonoBehaviour
{
    [SerializeField]
    private EquipmentCategory armorCategory;
    [SerializeField]
    private string subCategory;
    [SerializeField]
    public MeshFilter itemMeshFilter;
    [SerializeField]
    private int indexOverLapping;

    public int IndexOverLapping => indexOverLapping;

    private void Start()
    {
        itemMeshFilter = GetComponent<MeshFilter>();
    }

    public EquipmentCategory ArmorCategory => armorCategory;
    public string SubCategory => subCategory;

    public void ChangeFilter(MeshFilter changeFilter)
    {
        if (itemMeshFilter == null)
        {
            return;
        }

        if(changeFilter == null)
        {
            itemMeshFilter.sharedMesh = null;
        }
        else
        {
            itemMeshFilter.sharedMesh = changeFilter.sharedMesh;
        }
    }
}
