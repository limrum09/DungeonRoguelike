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

    private void Start()
    {
        itemMeshFilter = GetComponent<MeshFilter>();
    }

    public EquipmentCategory ArmorCategory => armorCategory;
    public string SubCategory => subCategory;

    public void ChangeFilter(MeshFilter changeFilter)
    {
        if (itemMeshFilter != null && changeFilter != null)
        {
            itemMeshFilter.sharedMesh = changeFilter.sharedMesh;
        }
    }
}
