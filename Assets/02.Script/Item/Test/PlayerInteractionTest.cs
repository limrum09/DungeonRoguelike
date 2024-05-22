using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    [SerializeField]
    private bool armorOverLapping;
    [SerializeField]
    private int armorIndexOverLapping;

    public Animator PlayerAnimator => animator;
    public GameObject WeaponR => weaponR;
    public GameObject WeaponL => weaponL;

    private void Start()
    {
        armorOverLapping = true;
        armorIndexOverLapping = 9999;
        GameManager.instance.InteractionTest(this);
    }

    public void WeaponChange(SelectTest weapon)
    {

    }

    public void ArmorChange(ArmorSelectTest armor)
    {
        foreach(var items in armors)
        {
            if (armor.EquipmentCategory == items.ArmorCategory)
            {
                MeshFilter changeFilter = null;
                
                if (armor.ItemOverlapping == false)
                {
                    
                    items.ChangeFilter(changeFilter);
                }
                if (armor.SubCateogy == items.SubCategory)
                {
                    if (armor.ItemPrefab != null)
                    {
                        changeFilter = armor.ItemPrefab.GetComponent<MeshFilter>();
                    }

                    items.ChangeFilter(changeFilter);
                }
            }
        }
    }
    public void ArmorChange(ArmorSelect armor)
    {
        bool onView;
        bool indexView;
        foreach (var items in armors)
        {
            if (armor.EquipmentCategory == items.ArmorCategory)
            {
                indexView = true;
                onView = armor.ItemOverlapping;

                if (!armor.ItemOverlapping)
                {
                    armorIndexOverLapping = armor.IndexOverlapping;
                }
                else if(armor.ItemOverlapping)
                {
                    if(armor.IndexOverlapping > armorIndexOverLapping)
                    {
                        indexView = false;
                    }
                }

                if (armor.SubCateogy == items.SubCategory)
                {
                    onView = indexView;

                    MeshFilter changeFilter = null;

                    if (armor.ItemPrefab != null)
                    {
                        changeFilter = armor.ItemPrefab.GetComponent<MeshFilter>();
                    }
                    else if(changeFilter == null && armor.IndexOverlapping == armorIndexOverLapping)
                    {
                        armorIndexOverLapping = 9999;
                    }

                    items.ChangeFilter(changeFilter);
                }

                items.gameObject.SetActive(onView);
            }
        }
    }
}
