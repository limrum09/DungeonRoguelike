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
    private List<ItemInfoInItemStatus> weaponItems;
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

    public void LoadItemStatus()
    {
        Debug.Log("Load Item Status");
        foreach(var weapon in weaponItems)
        {
            Debug.Log("Searching Weapons");
            if (weapon.SelectItemPart.WeaponItem != null)
            {
                Debug.Log("Change Weapon");
                ChangeWaeponItem(weapon.SelectItemPart.WeaponItem);
            }
        }

        foreach(var armor in armorItems)
        {
            if(armor.SelectItemPart.ArmorItem != null)
            {

                ChangeArmorItem(armor.SelectItemPart.ArmorItem);
            }
        }

        SetItemStatus();
    }

    public void ChangeWaeponItem(WeaponSelect weapon)
    {
        WeaponSelect newWeapon = weapon;
        ItemPartStatus itemStatus = null;

        interactionTest.WeaponChange(newWeapon);

        if (newWeapon.ItemPrefab)
        {
            itemStatus = newWeapon.ItemPrefab.GetComponent<ItemPartStatus>();
        }
        else
        {
            Debug.Log("This item have not 'ItemPartStatus'.");
        }

        if (!newWeapon.LeftWeapon)
        {
            weaponItems[1].SelectItemPart.ChangeItem(newWeapon, itemStatus);
            if (!newWeapon.UseOndeHand)
                weaponItems[0].SelectItemPart.ChangeItem(newWeapon, itemStatus);
        }
        else
        {
            weaponItems[0].SelectItemPart.ChangeItem(newWeapon, itemStatus);
        }

        Debug.Log("Change Weapon Item");
        SetItemStatus();
    }

    public void ChangeArmorItem(ArmorSelect item)
    {
        ArmorSelect newArmor = item;
        ItemPartStatus itemStatus = null;

        interactionTest.ArmorChange(newArmor);

        if (newArmor.ItemPrefab)
        {
            itemStatus = newArmor.ItemPrefab.GetComponent<ItemPartStatus>();
        }
        else
        {
            Debug.Log("This item have not 'ItemPartStatus'.");
        }

        foreach(ArmorItemInfoInItemStatus changeItemInfo in armorItems)
        {
            if(changeItemInfo.ItemCategory == newArmor.EquipmentCategory && changeItemInfo.SubCategory == newArmor.SubCateogy)
            {
                if (itemStatus != null) changeItemInfo.SelectItemPart.ChangeItem(newArmor, itemStatus);

                SetItemStatus();
            }
        }
    }

    private void SetItemStatus()
    {
        ResetItemStatus();
        
        foreach (ArmorItemInfoInItemStatus item in armorItems)
        {
            if(item.SelectItemPart.StatusItemPart != null)
            {
                // Debug.Log("Item Name : " + item);

                ItemPartStatus changeItem = item.SelectItemPart.StatusItemPart;

                itemHp += changeItem.ItemHP;
                itemDamage += changeItem.ItemDamage;
                itemCriticalDamage += changeItem.ItemCriticalDamage;
                itemSheild += changeItem.ItemSheild;
                itemCriticalPer += changeItem.ItemCriticalPer;
                itemSpeed += changeItem.ItemSpeed;
                itemCoolTime += changeItem.ItemCoolTime;
            }
        }

        foreach(ItemInfoInItemStatus item in weaponItems)
        {
            if(item.SelectItemPart.StatusItemPart != null)
            {
                ItemPartStatus changeItem = item.SelectItemPart.StatusItemPart;

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
