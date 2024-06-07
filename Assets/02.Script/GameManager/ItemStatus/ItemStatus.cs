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
    }

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveFile");

        if (!File.Exists(path))
        {
         
            // Have not Load File
        }

        GameManager.instance.ChangeExpBar();
    }

    // ���� ���� ��, ���� ���� �� ���� ����
    public void ChangeWeaponItem(WeaponItem weapon)
    {
        WeaponItem newWeapon = weapon;

        // ���� ������ �������� player�� �����ϱ�
        interactionTest.WeaponChange(newWeapon);

        // �÷��̾��� �޼� ����(����, �Ѽհ�)�� �ƴ� ���
        if (!newWeapon.LeftWeapon)
        {
            weaponItems[0].weaponItem = newWeapon;
            // �μ� �̻� ����ϴ� ����
            if (!newWeapon.UseOndeHand)
            {
                weaponItems[1].weaponItem = null;
            }
        }
        else
        {
            weaponItems[1].weaponItem = newWeapon;
        }

        SetItemStatus();
    }

    // �� ���� ��, �� ���� �� ���� ����
    public void ChangeArmorItem(ArmorItem item)
    {
        ArmorItem newArmor = item;

        // ���� ������ Item�� �÷��̾ �����ϱ�
        interactionTest.ArmorChange(newArmor);

        if (!newArmor.ArmorItemObject)
        {
            SetItemStatus();
            Debug.Log("This item have not object(prefab).");
            return;
        }

        foreach (ArmorItemInfoInItemStatus changeItemInfo in armorItems)
        {
            // ��ü ī�װ��� ���� ī�װ��� ���Ͽ� Ȯ��
            if (changeItemInfo.ItemCategory == newArmor.EquipmentCategory && changeItemInfo.SubCategory == newArmor.SubCategory)
                    changeItemInfo.armorItem = newArmor;
        }

        SetItemStatus();
    }

    // ȣ�� ��, ��ü ������ ���� ����
    private void SetItemStatus()
    {
        ResetItemStatus();
        
        foreach (ArmorItemInfoInItemStatus item in armorItems)
        {
            // Item Database���� �˸��� itemCode�� ã�Ƽ� ������ ������Ű��
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

    // ���� �ʱ�ȭ
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
