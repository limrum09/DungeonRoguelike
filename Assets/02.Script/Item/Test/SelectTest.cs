using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SelectTest : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private EquipmentCategory equipmentCategory;

    public EquipmentCategory EquipmentCategory => equipmentCategory;
    public GameObject ItemPrefab => prefab;

    public void SelectItemButton(GameObject selectObject)
    {
        prefab = selectObject;

        UIAndSceneManager.instance.ChangeEquipment(this);
    }

    public abstract SelectTest TestClone();
}
