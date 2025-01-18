using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemStatus : MonoBehaviour
{
    [Header("Items Total Status")]
    [SerializeField]
    private int itemHp;
    [SerializeField]
    private int itemDamage;
    [SerializeField]
    private int itemCriticalDamage;
    [SerializeField]
    private int itemSheild;
    [SerializeField]
    private float itemCriticalPer;
    [SerializeField]
    private float itemSpeed;
    [SerializeField]
    private float itemCoolTime;

    [Header("Script")]
    [SerializeField]
    private List<WeaponItemInfoInItemStatus> weaponItems;
    [SerializeField]
    private List<ArmorItemInfoInItemStatus> armorItems;
    [SerializeField]
    private PlayerInteractionTest interactionTest;

    [Header("None Weapon")]
    [SerializeField]
    private WeaponItem rightNone;
    [SerializeField]
    private WeaponItem leftNone;


    public int ItemHP { get { return itemHp; } set { itemHp = value; } }
    public int ItemDamage { get { return itemDamage; } set { itemDamage = value; } }
    public int ItemCriticalDamage { get { return itemCriticalDamage; } set { itemCriticalDamage = value; } }
    public int ItemSheid { get { return itemSheild; } set { itemSheild = value; } }
    public float ItemCriticalPer { get { return itemCriticalPer; } set { itemCriticalPer = value; } }
    public float ItemSpeed { get { return itemSpeed; } set { itemSpeed = value; } }
    public float ItemCoolTime { get { return itemCoolTime; } set { itemCoolTime = value; } }


    public List<WeaponItemInfoInItemStatus> WeaponItems => weaponItems;
    public List<ArmorItemInfoInItemStatus> ArmorItems => armorItems;

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        var playerInteraction = FindObjectOfType<PlayerInteractionTest>();
        if (playerInteraction != null)
        {
            InteractionTest(playerInteraction);
        }
    }

    public void InteractionTest(PlayerInteractionTest test)
    {
        this.interactionTest = test;
        interactionTest.PlayerInterationStart();
    }

    // 무기 변경 시, 무기 저장 및 스탯 변경
    public void ChangeWeaponItem(WeaponItem weapon)
    {
        WeaponItem newWeapon = weapon;

        Manager.Instance.UIAndScene.Notion.SetNotionText($"{newWeapon.ItemName}으로 무기 변경");
        // 새로 가져온 아이템을 player가 장착하기
        interactionTest.WeaponChange(newWeapon);

        // 플레이어의 왼손 무기(방패, 한손검)가 아닐 경우
        if (!newWeapon.LeftWeapon)
        {
            weaponItems[0].weaponItem = newWeapon;
            // 두손 이상 사용하는 무기 || 오른손 무기가 완드일 경우
            if (!newWeapon.UseOndeHand || newWeapon.WeaponValue == 5)
            {
                // 왼손 무기 None
                weaponItems[1].weaponItem = leftNone;
            }
        }
        else
        {
            weaponItems[1].weaponItem = newWeapon;

            // 왼손의 무기가 '검' 또는 '방패' 일 경우, 오른손에 '검'이 아닌 다른 것이 들려있다면, 오른손의 무기 값에 null을 넣는다.
            if (newWeapon.WeaponValue == 1 || newWeapon.WeaponValue == 2)
                if (weaponItems[0].weaponItem != null)
                    if (weaponItems[0].weaponItem.WeaponValue != 1)
                        // 오른손 무기 None
                        weaponItems[0].weaponItem = rightNone;
        }

        SetItemStatus();
    }

    // 방어구 변경 시, 방어구 저장 및 스탯 변경
    public void ChangeArmorItem(ArmorItem item)
    {
        ArmorItem newArmor = item;

        // 새로 가져온 Item을 플레이어가 착용하기
        interactionTest.ArmorChange(newArmor);

        if (!newArmor.ItemObject)
        {
            // Debug.LogError("This item have not object(prefab). : " + newArmor.name);
        }

        foreach (ArmorItemInfoInItemStatus changeItemInfo in armorItems)
        {
            // 전체 카테고리와 서브 카테고리를 비교하여 확인
            if (changeItemInfo.ItemCategory == newArmor.EquipmentCategory && changeItemInfo.SubCategory == newArmor.SubCategory)
            {
                changeItemInfo.armorItem = newArmor;
            }
                    
        }

        SetItemStatus();
    }

    // 호출 시, 전체 아이템 스탯 정정
    private void SetItemStatus()
    {
        ResetItemStatus();
        
        foreach (ArmorItemInfoInItemStatus item in armorItems)
        {
            // Item Database에서 알맞은 itemCode를 찾아서 스텟을 증가시키기
            if(item.armorItem != null)
            {
                ArmorItem changeItem = item.armorItem;

                itemHp += changeItem.ItemHP;
                itemDamage += changeItem.ItemDamage;
                itemCriticalDamage += changeItem.ItemCriticalDamage;
                itemSheild += changeItem.ItemSheild;
                itemCriticalPer += changeItem.ItemCriticalPer;
                itemSpeed += changeItem.ItemSpeed;
                itemCoolTime += changeItem.ItemCoolTime;
            }
        }
         
        foreach(WeaponItemInfoInItemStatus item in weaponItems)
        {
            if(item.weaponItem != null)
            {
                WeaponItem changeItem = item.weaponItem;

                itemHp += changeItem.ItemHP;
                itemDamage += changeItem.ItemDamage;
                itemCriticalDamage += changeItem.ItemCriticalDamage;
                itemSheild += changeItem.ItemSheild;
                itemCriticalPer += changeItem.ItemCriticalPer;
                itemSpeed += changeItem.ItemSpeed;
                itemCoolTime += changeItem.ItemCoolTime;
            }
        }

        Manager.Instance.Game.ChangePlayerStatus();
    }

    // 스탯 초기화
    private void ResetItemStatus()
    {
        itemHp = 0;
        itemDamage = 0;
        itemCriticalDamage = 0;
        itemSheild = 0;
        itemCriticalPer = 0f;
        itemSpeed = 0f;
        itemCoolTime = 0f;
    }
}
