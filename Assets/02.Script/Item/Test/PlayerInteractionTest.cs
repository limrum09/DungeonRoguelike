using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionTest : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponR;
    [SerializeField]
    private GameObject weaponL;
    [SerializeField]
    private ItemCategory[] armors;


    public void WeaponChange(SelectTest weapon)
    {

    }

    public void ArmorChange(ArmorSelectTest item)
    {
        foreach(var items in armors)
        {
            if(item.EquipmentCategory == items.itemCategory)
            {

            }
        }
    }
}
