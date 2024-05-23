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

        itemMeshFilter.sharedMesh = changeFilter != null ? changeFilter.sharedMesh : null;
    }
}
