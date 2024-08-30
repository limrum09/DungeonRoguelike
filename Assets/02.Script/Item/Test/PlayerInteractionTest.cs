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
    [SerializeField]
    private MeshCollider attackMesh;

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
            attackMesh.sharedMesh = currentWeaponFilter.sharedMesh;
        }
    }
     
    public void ArmorChange(ArmorItem armor)
    {
        ArmorItem newArmor = armor;

        bool onView;

        foreach (var items in armors)
        {
            if (newArmor.EquipmentCategory != items.ArmorCategory) continue;
            if (newArmor.SubCategory != items.SubCategory) continue;

            onView = true;
            items.overLapping = newArmor.ItemOverlapping;

            // 겹쳐서 착용 불가능한 아이템
            if (!newArmor.ItemOverlapping)
            {
                armorIndexOverLapping = newArmor.IndexOverlapping;

                OtherArmorView(newArmor, false);
            }
            else
            {
                OtherArmorView(newArmor, true);
                // 겹쳐서 사용 가능한 아이템 중, 현제 사용중인 아이템의 IndexOverLapping보다 값이 큰 경우 보이지 않게한다.
                if (newArmor.IndexOverlapping > armorIndexOverLapping)
                {
                     onView = false;
                }
            }

            if (newArmor.ItemOverlapping && newArmor.IndexOverlapping == armorIndexOverLapping)
            {
                armorIndexOverLapping = 9999;
            }

            // Item이 None인 경우, 아이템 착용 취소
            if (!newArmor.HaveItem)
            {
                onView = false;
                OtherArmorView(newArmor, true);
            }

            // SubCategory가 같은 경우에만 동작
            if (newArmor.SubCategory == items.SubCategory)
            {
                // 착용할 Item에 Meshfilter를 가지고 있다면 해당 MeshFilter를 가져온다.
                // 없다면 교환 불가
                MeshFilter changeFilter = newArmor.ArmorItemObject != null ? newArmor.ArmorItemObject.GetComponent<MeshFilter>() : null;

                if (changeFilter == null && newArmor.IndexOverlapping == armorIndexOverLapping)
                {
                    armorIndexOverLapping = 9999;
                }

                items.ChangeFilter(changeFilter);
                items.gameObject.SetActive(onView);
            }
        }
    }

    // 현제 아이템의 대단위 Category가 같고, SubCategory가 다른 아이템들의 view값에 따라 착용 여부를 결정한다.
    private void OtherArmorView(ArmorItem armor, bool view)
    {
        // Debug.Log("현제 아이템 : " + armor.ItemCode + ", 겹치기 가능 여부 : " + view);
        foreach (var item in armors)
        {
            if (item.ArmorCategory == armor.EquipmentCategory)
                if (item.SubCategory != armor.SubCategory && item.overLapping)
                    item.gameObject.SetActive(view);
        }
    }
}
