using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SelectTest : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefab;
    [SerializeField]
    protected EquipmentCategory equipmentCategory;
    [SerializeField]
    protected bool itemOverlapping;

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
