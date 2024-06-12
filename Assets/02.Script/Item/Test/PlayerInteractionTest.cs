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

    public GameObject WeaponR => weaponR;
    public GameObject WeaponL => weaponL;

    private void Start()
    {
        armorIndexOverLapping = 9999;

        meshFilterWeaponR = weaponR.gameObject.GetComponent<MeshFilter>();
        meshFilterWeaponL = weaponL.gameObject.GetComponent<MeshFilter>();
    }



    public void WeaponChange(WeaponItem weapon)
    {
        WeaponItem newWeapon = weapon;
        ItemRotation weaponRotation = newWeapon.WeaponItemObject != null ? newWeapon.WeaponItemObject.GetComponent<ItemRotation>() : null;

        // weaponROneHand 값을 어디선가 초기화 해줘야한다. 게임 시작할 때 무기 가지고 있는지 확인작업 필요한 듯
        // Left Weapon
        if (newWeapon.LeftWeapon)
        {
            useWeaponL = true;
            WeaponL.SetActive(true);

            if (weaponRotation != null)
            {
                WeaponL.transform.localRotation = Quaternion.Euler(weaponRotation.PlayerItemRotationX, weaponRotation.PlayerItemRotationY * -1.0f, weaponRotation.PlayerItemRotationZ + 180f);
            }

            // player don't select left weapon
            if (!newWeapon.WeaponItemObject)
            {
                useWeaponL = false;
                // condition ? true : false
                tempSaveLeftWeaponAnimatorController = tempSaveRightWeaponAnimatorController != null ? tempSaveRightWeaponAnimatorController : defaultAnimatorController;
            }
            else
            {
                tempSaveLeftWeaponAnimatorController = newWeapon.WeaponAnimator;
            }

            animator.runtimeAnimatorController = weaponROneHand ? tempSaveLeftWeaponAnimatorController : defaultAnimatorController;

            if (!weaponROneHand)
            {
                WeaponR.SetActive(false);
            }
        }
        // Right Wepon
        else
        {
            if (weaponRotation != null)
            {
                WeaponR.transform.localRotation = Quaternion.Euler(weaponRotation.PlayerItemRotationX, weaponRotation.PlayerItemRotationY, weaponRotation.PlayerItemRotationZ);
            }


            weaponR.SetActive(true);
            weaponROneHand = newWeapon.UseOndeHand;

            tempSaveRightWeaponAnimatorController = newWeapon.WeaponItemObject != null ? newWeapon.WeaponAnimator : defaultAnimatorController;

            animator.runtimeAnimatorController = tempSaveRightWeaponAnimatorController;

            // When player doesn't have left weapon after select right weapon
            if (!useWeaponL)
            {
                weaponL.SetActive(false);
            }
            // When player have left weapon
            else if (!newWeapon.UseOndeHand)
            {
                // Select weapon is using two hands
                useWeaponL = false;
                weaponL.SetActive(false);
            }
            // Using one hand. Applying the left weapon's animator
            else if (newWeapon.UseOndeHand)
            {
                animator.runtimeAnimatorController = newWeapon.WeaponItemObject != null ? tempSaveLeftWeaponAnimatorController : defaultAnimatorController;
            }
        }

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
