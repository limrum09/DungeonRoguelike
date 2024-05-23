using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteractionTest : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController defaultAnimatorController;
    [SerializeField]
    private GameObject weaponR;
    [SerializeField]
    private GameObject weaponL;

    [Header("Armor")]
    [SerializeField]
    private ArmorsCategory[] armors;
    [SerializeField]
    private int armorIndexOverLapping;

    private bool weaponROneHand;
    private bool useWeaponL;
    private MeshFilter meshFilterWeaponR;
    private MeshFilter meshFilterWeaponL;
    private RuntimeAnimatorController tempSaveLeftWeaponAnimatorController;
    private RuntimeAnimatorController tempSaveRightWeaponAnimatorController;

    public Animator PlayerAnimator => animator;
    public GameObject WeaponR => weaponR;
    public GameObject WeaponL => weaponL;

    private void Start()
    {
        armorIndexOverLapping = 9999;
        GameManager.instance.InteractionTest(this);

        meshFilterWeaponR = weaponR.gameObject.GetComponent<MeshFilter>();
        meshFilterWeaponL = weaponL.gameObject.GetComponent<MeshFilter>();
    }

    public void WeaponChange(WeaponSelect weapon)
    {
        WeaponSelect newWeapon = weapon;
            
        // Left Weapon
        if (newWeapon.LeftWeapon)
        {
            useWeaponL = true;
            WeaponL.SetActive(true);

            // player don't select left weapon
            if (!newWeapon.ItemPrefab)
            {
                useWeaponL = false;
                // condition ? true : false
                tempSaveLeftWeaponAnimatorController = tempSaveRightWeaponAnimatorController != null ? tempSaveRightWeaponAnimatorController : defaultAnimatorController;
            }
            else
            {
                tempSaveLeftWeaponAnimatorController = newWeapon.WeaponAnimator;
            }

            
            if (weaponROneHand)
            {
                animator.runtimeAnimatorController = tempSaveLeftWeaponAnimatorController;
            }
            else
            {
                WeaponR.SetActive(false);
                animator.runtimeAnimatorController = defaultAnimatorController;
            }
        }
        // Right Wepon
        else
        {
            weaponR.SetActive(true);
            weaponROneHand = newWeapon.UseOndeHand;

            tempSaveRightWeaponAnimatorController = newWeapon.ItemPrefab != null ? newWeapon.WeaponAnimator : defaultAnimatorController;

            // When player doesn't have left weapon after select right weapon
            if (!useWeaponL)
            {
                weaponL.SetActive(false);
                animator.runtimeAnimatorController = tempSaveRightWeaponAnimatorController;
            }
            else
            {
                if (!newWeapon.UseOndeHand)
                {
                    useWeaponL = false;
                    weaponL.SetActive(false);
                    animator.runtimeAnimatorController = tempSaveRightWeaponAnimatorController;
                }
                else
                {
                    Debug.Log("Use Weaopn two");
                    animator.runtimeAnimatorController = tempSaveLeftWeaponAnimatorController;
                }
            }
                
        }

        MeshFilter changeFilter = newWeapon.ItemPrefab != null ? newWeapon.ItemPrefab.GetComponent<MeshFilter>() : null;
        MeshFilter currentWeaponFilter = newWeapon.LeftWeapon ? meshFilterWeaponL : meshFilterWeaponR;
        currentWeaponFilter.sharedMesh = changeFilter != null ? changeFilter.sharedMesh : null;

        //weaponR.SetActive(weaponROneHand);

    }
    public void ArmorChange(ArmorSelect armor)
    {
        ArmorSelect newArmor = armor;

        bool onView;
        bool indexView;
        foreach (var items in armors)
        {
            if (newArmor.EquipmentCategory == items.ArmorCategory)
            {
                indexView = true;
                onView = newArmor.ItemOverlapping;

                if (!newArmor.ItemOverlapping)
                {
                    armorIndexOverLapping = newArmor.IndexOverlapping;
                }
                else if(newArmor.ItemOverlapping)
                {
                    if(newArmor.IndexOverlapping > armorIndexOverLapping)
                    {
                        indexView = false;
                    }
                    else if(newArmor.IndexOverlapping == armorIndexOverLapping)
                    {
                        armorIndexOverLapping = 9999;
                    }
                }

                if (newArmor.SubCateogy == items.SubCategory)
                {
                    onView = indexView;

                    MeshFilter changeFilter = null;

                    if (newArmor.ItemPrefab != null)
                    {
                        changeFilter = newArmor.ItemPrefab.GetComponent<MeshFilter>();
                    }
                    else if(changeFilter == null && newArmor.IndexOverlapping == armorIndexOverLapping)
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
