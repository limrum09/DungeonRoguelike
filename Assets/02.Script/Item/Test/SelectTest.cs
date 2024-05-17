using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectTest : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private EquipmentCategory equipmentCategory;

    public EquipmentCategory EquipmentCategory => equipmentCategory;

    public void SelectItemButton(GameObject selectObject)
    {
        prefab = selectObject;

        UIAndSceneManager.instance.ChangeEquipment(this);
    }

    public SelectTest TestClone()
    {
        return this;
    }
}
