using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemStatus : MonoBehaviour
{
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
    [SerializeField]
    private List<WeaponItemInfoInItemStatus> weaponItems;
    [SerializeField]
    private List<ArmorItemInfoInItemStatus> armorItems;
    [SerializeField]
    private PlayerInteractionTest interactionTest;

    public int ItemHP { get { return itemHp; } set { itemHp = value; } }
    public int ItemDamage { get { return itemDamage; } set { itemDamage = value; } }
    public int ItemCriticalDamage { get { return itemCriticalDamage; } set { itemCriticalDamage = value; } }
    public int ItemSheid { get { return itemSheild; } set { itemSheild = value; } }
    public float ItemCriticalPer { get { return itemCriticalPer; } set { itemCriticalPer = value; } }
    public float ItemSpeed { get { return itemSpeed; } set { itemSpeed = value; } }
    public float ItemCoolTime { get { return itemCoolTime; } set { itemCoolTime = value; } }

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
    }

    private void Start()
    {
        LoadItemStatus();

        GameManager.instance.ChangeExpBar();
    }

    public void PlayerAddItemStatus()
    {
        GameManager.instance.ChangePlayerStatus();
    }

    public void PlayerLostItemStatus()
    {
        GameManager.instance.ChangePlayerStatus();
    }

    // 아이템이 저장 되어 있을 시 가져오기
    public void LoadItemStatus()
    {
        /*foreach(var weapon in weaponItems)
        {
            if (weapon.SelectItemPart.WeaponItem != null)
            {
                ChangeWaeponItem(weapon.SelectItemPart.WeaponItem);
            }
        }*/

        SetItemStatus();
    }

    // 무기 변경 시, 무기 저장 및 스탯 변경
    public void ChangeWeaponItem(WeaponItem weapon)
    {
        WeaponItem newWeapon = weapon;

        interactionTest.WeaponChange(newWeapon);

        // 프리팹 있는지 확인
        /*f (!newWeapon.WeaponItemObject)
        {
            SetItemStatus();
            return;
        }*/

        // 플레이어의 왼손 무기(방패, 한손검)가 아닐 경우
        if (!newWeapon.LeftWeapon)
        {
            weaponItems[1].weaponItem = newWeapon;
            // 두손 이상 사용하는 무기
            if (!newWeapon.UseOndeHand)
            {
                weaponItems[0].weaponItem = null;
            }
        }
        else
        {
            weaponItems[0].weaponItem = newWeapon;
        }

        SetItemStatus();
    }

    // 방어구 변경 시, 방어구 저장 및 스탯 변경
    public void ChangeArmorItem(ArmorItem item)
    {
        ArmorItem newArmor = item;

        interactionTest.ArmorChange(newArmor);

        if (!newArmor.ArmorItemObject)
        {
            SetItemStatus();
            Debug.Log("This item have not object(prefab).");
            return;
        }

        foreach (ArmorItemInfoInItemStatus changeItemInfo in armorItems)
        {
            // 전체 카테고리와 서브 카테고리를 비교하여 확인
            if (changeItemInfo.ItemCategory == newArmor.EquipmentCategory && changeItemInfo.SubCategory == newArmor.SubCategory)
            {
                changeItemInfo.armorItem = newArmor;

                SetItemStatus();
            }
        }
    }

    // 호출 시, 전체 아이템 스탯 정정
    private void SetItemStatus()
    {
        ResetItemStatus();
        
        foreach (ArmorItemInfoInItemStatus item in armorItems)
        {
/*            if(item.SelectItemPart.StatusItemPart != null)
            {
                ItemPartStatus changeItem = item.SelectItemPart.StatusItemPart;

                itemHp += changeItem.ItemHP;
                itemDamage += changeItem.ItemDamage;
                itemCriticalDamage += changeItem.ItemCriticalDamage;
                itemSheild += changeItem.ItemSheild;
                itemCriticalPer += changeItem.ItemCriticalPer;
                itemSpeed += changeItem.ItemSpeed;
                itemCoolTime += changeItem.ItemCoolTime;
            }*/

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

        GameManager.instance.ChangePlayerStatus();
        GameManager.instance.ChangeHPBar();
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
