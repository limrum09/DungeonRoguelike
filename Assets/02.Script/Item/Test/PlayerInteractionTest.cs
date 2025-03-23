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

    public GameObject WeaponR => weaponR;
    public GameObject WeaponL => weaponL;

    private List<GameObject> weaponRChilds = new List<GameObject>();
    private List<GameObject> weaponLChilds = new List<GameObject>();

    public void PlayerInterationStart()
    {
        armorIndexOverLapping = 9999;

        int count = 0;

        count = weaponR.transform.childCount;
        for (int i = 0; i < count; i++)
            weaponRChilds.Add(weaponR.transform.GetChild(i).gameObject);

        count = weaponL.transform.childCount;
        for (int i = 0; i < count; i++)
            weaponLChilds.Add(weaponL.transform.GetChild(i).gameObject);
    }

    public void WeaponChange(WeaponItem weapon)
    {
        WeaponItem newWeapon = weapon;

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
            string weaponName = newWeapon.ItemObject != null ? newWeapon.ItemObject.name : "None";

            foreach (var childWeapon in weaponRChilds)
            {
                if (string.Equals(weaponName, childWeapon.name))
                    childWeapon.SetActive(true);
                else if (childWeapon.activeSelf)
                    childWeapon.SetActive(false);
            }
        }            
        else
        {
            weaponL.SetActive(true);
            leftWeaponValue = weaponAniValue;

            string weaponName = newWeapon.ItemObject != null ? newWeapon.ItemObject.name : "None";

            foreach (var childWeapon in weaponLChilds)
            {
                if (string.Equals(weaponName, childWeapon.name))
                    childWeapon.SetActive(true);
                else if (childWeapon.activeSelf)
                    childWeapon.SetActive(false);
            }

            // 현제 왼손에는 검과 방패만 사용이 가능한데, 두 무기다 오른손에는 한손검만 사용한다.
            // 이에 오른손 무기가 한손검이 아닐경우 오른손 무기를 사용할 수 없다.
            if (rightWeaponValue != 1 && !weaponName.Contains("None"))
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

    public void SwimStart()
    {
        weaponR.SetActive(false);
        WeaponL.SetActive(false);
    }

    public void SwimEnd()
    {
        weaponR.SetActive(true);
        WeaponL.SetActive(true);
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
