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
    private GameObject weaponR;
    [SerializeField]
    private GameObject weaponL;

    [Header("Armor")]
    [SerializeField]
    private ArmorsCategory[] armors;
    [SerializeField]
    private int armorIndexOverLapping;

    private int rightWeaponValue;
    private int leftWeaponValue;
    private MeshFilter meshFilterWeaponR;
    private MeshFilter meshFilterWeaponL;

    public GameObject WeaponR => weaponR;
    public GameObject WeaponL => weaponL;

    public void PlayerInterationStart()
    {
        armorIndexOverLapping = 9999;

        meshFilterWeaponR = weaponR.gameObject.GetComponent<MeshFilter>();
        meshFilterWeaponL = weaponL.gameObject.GetComponent<MeshFilter>();
    }

    public void WeaponChange(WeaponItem weapon)
    {
        WeaponItem newWeapon = weapon;
        ItemRotation weaponRotation = newWeapon.WeaponItemObject != null ? newWeapon.WeaponItemObject.GetComponent<ItemRotation>() : null;

        int weaponAniValue = newWeapon.WeaponValue;

        if (!newWeapon.UseOndeHand)
        {
            leftWeaponValue = 0;
            weaponL.SetActive(false);
        }

        if (!newWeapon.LeftWeapon)
        {
            weaponR.SetActive(true);
            rightWeaponValue = weaponAniValue;

            if (weaponRotation != null)
            {
                WeaponR.transform.localRotation = Quaternion.Euler(weaponRotation.PlayerItemRotationX, weaponRotation.PlayerItemRotationY, weaponRotation.PlayerItemRotationZ);
            }
        }            
        else
        {
            weaponL.SetActive(true);
            leftWeaponValue = weaponAniValue;

            if (weaponRotation != null)
            {
                WeaponL.transform.localRotation = Quaternion.Euler(weaponRotation.PlayerItemRotationX, weaponRotation.PlayerItemRotationY * -1.0f, weaponRotation.PlayerItemRotationZ + 180f);
            }

            if (rightWeaponValue != 1)
            {
                rightWeaponValue = 0;
                weaponR.SetActive(false);
            }
        }
            

        animator.SetInteger("RightWeaponValue", rightWeaponValue);
        animator.SetInteger("LeftWeaponValue", leftWeaponValue);

        animator.SetFloat("Forward", 0.0f);
        animator.SetFloat("Rotation", 0.0f);
        animator.SetBool("Jump", false);
        animator.SetBool("Walk", false);
        animator.SetBool("IsAttack", false);

        MeshFilter changeFilter = newWeapon.WeaponItemObject != null ? newWeapon.WeaponItemObject.GetComponent<MeshFilter>() : null;
        MeshFilter currentWeaponFilter = newWeapon.LeftWeapon ? meshFilterWeaponL : meshFilterWeaponR;
        Debug.Log("Change Filter : " + changeFilter + ", Current Weapon Filter : " + currentWeaponFilter);
        currentWeaponFilter.sharedMesh = changeFilter != null ? changeFilter.sharedMesh : null;

        if (!newWeapon.LeftWeapon && currentWeaponFilter == meshFilterWeaponR)
        {
            MeshCollider meshCollider = weaponR.GetComponent<MeshCollider>();
            meshCollider.sharedMesh = currentWeaponFilter.sharedMesh;
        }
    }
     
    public void ArmorChange(ArmorItem armor)
    {
        ArmorItem newArmor = armor;

        bool onView;
        bool indexView;

        foreach (var items in armors)
        {
            if (newArmor.EquipmentCategory != items.ArmorCategory) continue;

            indexView = true;
            onView = newArmor.ItemOverlapping;

            if (!newArmor.ItemOverlapping)
            {
                armorIndexOverLapping = newArmor.IndexOverlapping;
            }
            else if (newArmor.IndexOverlapping > armorIndexOverLapping)
            {
                indexView = false;
            }
            else if (newArmor.IndexOverlapping == armorIndexOverLapping)
            {
                armorIndexOverLapping = 9999;
            }

            if (newArmor.SubCategory == items.SubCategory)
            {
                onView = indexView;

                MeshFilter changeFilter = newArmor.ArmorItemObject != null ? newArmor.ArmorItemObject.GetComponent<MeshFilter>() : null;

                if (changeFilter == null && newArmor.IndexOverlapping == armorIndexOverLapping)
                {
                    armorIndexOverLapping = 9999;
                }

                items.ChangeFilter(changeFilter);
            }

            items.gameObject.SetActive(onView);
        }
    }
}
