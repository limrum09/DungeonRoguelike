using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SelectTest : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private EquipmentCategory equipmentCategory;
    [SerializeField]
    private bool itemOverlapping;

    public EquipmentCategory EquipmentCategory => equipmentCategory;
    public GameObject ItemPrefab => prefab;
    public bool ItemOverlapping => itemOverlapping;

    public void SelectItemButton(GameObject selectObject, bool isOverLapping)
    {
        prefab = selectObject;
        itemOverlapping = isOverLapping;

        UIAndSceneManager.instance.ChangeEquipment(this);
    }

    public abstract SelectTest TestClone();
}
