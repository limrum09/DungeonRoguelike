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

        // 무기가 왼손, 오른손 위치에 따라 조금 회전을 줄 필요가 있음
        ItemRotation weaponRotation = newWeapon.ItemObject != null ? newWeapon.ItemObject.GetComponent<ItemRotation>() : null;

        // Animator의 무기 모션을 확인할 값
        int weaponAniValue = newWeapon.WeaponValue;

        // 한손으로 사용하는 무기가 아닌 경우
        if (!newWeapon.UseOndeHand)
        {
            leftWeaponValue = 0;
            weaponL.SetActive(false);
        }

        // 오른손에 사용하는 무기
        if (!newWeapon.LeftWeapon)
        {
            weaponR.SetActive(true);
            rightWeaponValue = weaponAniValue;

            // 무기에 회전을 주어야 하는 경우
            if (weaponRotation != null)
            {
                WeaponR.transform.localRotation = Quaternion.Euler(weaponRotation.PlayerItemRotationX, weaponRotation.PlayerItemRotationY, weaponRotation.PlayerItemRotationZ);
            }

            if (newWeapon.WeaponValue == 5)
                weaponL.SetActive(false);
        }            
        else
        {
            weaponL.SetActive(true);
            leftWeaponValue = weaponAniValue;

            if (weaponRotation != null)
            {
                WeaponL.transform.localRotation = Quaternion.Euler(weaponRotation.PlayerItemRotationX, weaponRotation.PlayerItemRotationY * -1.0f, weaponRotation.PlayerItemRotationZ + 180f);
            }

            // 현제 왼손에는 검과 방패만 사용이 가능한데, 두 무기다 오른손에는 한손검만 사용한다.
            // 이에 오른손 무기가 한손검이 아닐경우 오른손 무기를 사용할 수 없다.
            if (rightWeaponValue != 1)
            {
                rightWeaponValue = 0;
                weaponR.SetActive(false);
            }
        }
            
        // 무기 교체마다 Animator의 WeaponValue값을 변경하여 animation을 바꿔준다.
        animator.SetInteger("RightWeaponValue", rightWeaponValue);
        animator.SetInteger("LeftWeaponValue", leftWeaponValue);

        // 무기바꾸면 일단 초기화
        animator.SetFloat("Forward", 0.0f);
        animator.SetFloat("Rotation", 0.0f);
        animator.SetBool("Jump", false);
        animator.SetBool("Walk", false);
        animator.SetBool("IsAttack", false);

        // 무기를 장착 중이면, 현제 무기의 Mesh가져오기
        MeshFilter changeFilter = newWeapon.ItemObject != null ? newWeapon.ItemObject.GetComponent<MeshFilter>() : null;
        // 바궈야할 Mesh, 바꿀 무기가 왼손이면 player의 왼손 MeshFilter를 가져온다.
        MeshFilter currentWeaponFilter = newWeapon.LeftWeapon ? meshFilterWeaponL : meshFilterWeaponR;
        
        // 사용자에게 보여주는 Mesh
        currentWeaponFilter.sharedMesh = changeFilter != null ? changeFilter.sharedMesh : null;

        if (!newWeapon.LeftWeapon && currentWeaponFilter == meshFilterWeaponR)
        {
            // 실제로 몬스터와 상호작용하는 MeshCollider, 스킬을 사용할때 무기 판정 범위의 원활한 변경을 위해 따로 때에냄
            // 공격은 무조건 오른손에 착용한 무기가 상호작용한다.
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
                MeshFilter changeFilter = newArmor.ItemObject != null ? newArmor.ItemObject.GetComponent<MeshFilter>() : null;

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
