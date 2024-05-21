using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionTest : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject weaponR;
    [SerializeField]
    private GameObject weaponL;
    [SerializeField]
    private ItemCategory[] armors;

    public Animator PlayerAnimator => animator;
    public GameObject WeaponR => weaponR;
    public GameObject WeaponL => weaponL;

    private void Start()
    {
        GameManager.instance.InteractionTest(this);
    }

    public void WeaponChange(SelectTest weapon)
    {

    }

    public void ArmorChange(ArmorSelectTest armor)
    {
        foreach(var items in armors)
        {
            if(armor.EquipmentCategory == items.ArmorCategory)
            {
                if(armor.SubCateogy == items.SubCategory)
                {
//                    Debug.Log(armor.ItemPrefab.name);
                    MeshFilter changeFilter = armor.ItemPrefab.GetComponent<MeshFilter>();

                    items.ChangeFilter(changeFilter);
                }
            }
        }
    }
}
