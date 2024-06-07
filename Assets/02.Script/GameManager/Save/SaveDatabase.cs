using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDatabase : MonoBehaviour
{
    public static SaveDatabase instance;

    public SaveData saveData = new SaveData();

    private List<InvenItem> invenSlots;
    private ItemStatus itemStatus;

    [SerializeField]
    private InvenItemDatabase invenItemDatabase;
    [SerializeField]
    private WeaponItemDatabase weaponItemDatabase;
    [SerializeField]
    private ArmorItemDatabase armorItemDatabase;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        invenItemDatabase = Resources.Load<InvenItemDatabase>("InvenItemDatabase");
        weaponItemDatabase = Resources.Load<WeaponItemDatabase>("WeaponItemDatabase");
        armorItemDatabase = Resources.Load<ArmorItemDatabase>("ArmorItemDatabase");
    }


    private void OnApplicationQuit()
    {
        SaveData("SaveFile");
    }

    private void Start()
    {
        invenSlots = InvenData.instance.invenSlots;

        LoadData("SaveFile");
    }

    private void Initialzed()
    {
        saveData.inventoryCount = 0;
        saveData.itemCodes.Clear();
        saveData.itemCnts.Clear();
        saveData.slotIndexs.Clear();

        saveData.weaponItemCode.Clear();
        saveData.armorItemCode.Clear();

        saveData.level = 1;
        saveData.health = 5;
        saveData.str = 5;
        saveData.dex = 5;
        saveData.luk = 5;
        saveData.bonusState = 5;

        saveData.currnetHP = 125;
        saveData.exp = 200;
        saveData.currentExp = 0;
    }

    public void SaveData(string fileName)
    {
        Initialzed();

        var gM = GameManager.instance;

        saveData.level = gM.level;
        saveData.health = gM.health;
        saveData.str = gM.str;
        saveData.dex = gM.dex;
        saveData.luk = gM.luk;
        saveData.bonusState = gM.bonusState;


        var player = PlayerStatus.instance;

        saveData.currnetHP = player.CurrentHP;
        saveData.exp = player.Exp;
        saveData.currentExp = player.CurrentExp;

        itemStatus = FindObjectOfType<ItemStatus>();

        for(int i =0; i < itemStatus.WeaponItems.Count; i++)
        {
            // itemStatus.WeaponItems�� �޼հ� �������� ���� �迭�� ũ�Ⱑ 2�̴�. 
            // ���Ⱑ �⺻������ �����տ� �����ϸ� �Ѱ��� ���⸦ ����ϸ� �����տ� �����ȴ�.
            // �޼տ� ������ ���Ⱑ ���� ��� itemStatus.WeaponItems[i].weaponItem�� null���� ������.
            if(itemStatus.WeaponItems[i].weaponItem != null)
            {
                saveData.weaponItemCode.Add(itemStatus.WeaponItems[i].weaponItem.ItemCode);
            }            
        }

        for(int i = 0; i < itemStatus.ArmorItems.Count; i++)
        {
            // �������� �������� �ʴ� ��� itemStatus.ArmorItmes[i].armorItem�� ���� null���̴�.
            if(itemStatus.ArmorItems[i].armorItem != null)
            {
                saveData.armorItemCode.Add(itemStatus.ArmorItems[i].armorItem.ItemCode);
            }
        }

        // ���� �κ��丮�� ũ�⸦ ����
        saveData.inventoryCount = invenSlots.Count;
        for (int i = 0; i < saveData.inventoryCount; i++)
        {
            if(invenSlots[i] != null)
            {
                // ����Ǵ� ItemCode, itemCnt, index�� ���� �ٸ� �迭�� ���� ����ȴ�.
                // ���� �ٸ� �迭�� ���� index���� ������. (����� i�� ���� ����ȴ�.)
                saveData.itemCodes.Add(invenSlots[i].ItemCode);
                saveData.itemCnts.Add(invenSlots[i].itemCnt);
                saveData.slotIndexs.Add(i);
            }
        }

        // ������ ����
        string json = JsonUtility.ToJson(saveData);   // Json ����ȭ

        // ���� ��� ����
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // ���Ͽ� Json ���ڿ� ����
        File.WriteAllText(path, json);
    }


    public void LoadData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            // Json���ڿ��� �о����
            string loadJson = File.ReadAllText(path);

            // �ش� ��ġ�� InvenSaveData�� ��ȯ�� �����Ͱ� ������ ��������
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            var gM = GameManager.instance;

            gM.level = saveData.level;
            gM.health = saveData.health;
            gM.str = saveData.str;
            gM.dex = saveData.dex;
            gM.luk = saveData.luk;
            gM.bonusState = saveData.bonusState;


            var player = PlayerStatus.instance;

            player.CurrentHP = saveData.currnetHP;
            player.Exp = saveData.exp;
            player.CurrentExp = saveData.currentExp;


            for (int i = 0; i < saveData.weaponItemCode.Count; i++)
            {
                if (!string.IsNullOrEmpty(saveData.weaponItemCode[i]))
                {
                    // ItemCode�� ��ġ�ϴ� ���� ã��
                    WeaponItem item = SearchWeaponItem(saveData.weaponItemCode[i]);

                    // GameManager�� PlayerWeaponChange ȣ��
                    GameManager.instance.PlayerWeaponChange(item);
                }                
            }

            for (int i = 0; i < saveData.armorItemCode.Count; i++)
            {
                if (!string.IsNullOrEmpty(saveData.armorItemCode[i]))
                {
                    // ItemCode�� ��ġ�ϴ� �� ã��
                    ArmorItem item = SearchArmorItem(saveData.armorItemCode[i]);

                    // GameManager�� PlayerArmorChange ȣ��
                    GameManager.instance.PlayerArmorChange(item);
                }
            }


            // �κ��丮 �ʰ�ȭ
            InvenData.instance.Initialzed(saveData.inventoryCount);
            for (int i = 0; i < saveData.slotIndexs.Count; i++)
            {   
                if (!string.IsNullOrEmpty(saveData.itemCodes[i]))
                {
                    InvenItem loadItem = null;
                    // Item DB���� Code�� ���� �������� ã�� clone���� ����
                    loadItem = SearchInvenItem(saveData.itemCodes[i]).Clone();
                    // ã�� Item�� ���� ����
                    loadItem.itemCnt = saveData.itemCnts[i];
                    // ����Ǿ� �ִ� index�� �����ͼ� invenSlots�� index �߰�
                    invenSlots[saveData.slotIndexs[i]] = loadItem;
                    // �ش� �߰��� ������ ������ ���ΰ�ħ
                    InvenData.instance.RefreshInvenSlot(saveData.slotIndexs[i]);
                }
            }   
        }
    }

    private InvenItem SearchInvenItem(string itemCode) => invenItemDatabase.FindItemBy(itemCode);
    private WeaponItem SearchWeaponItem(string itemCode) => weaponItemDatabase.FindItemBy(itemCode);
    private ArmorItem SearchArmorItem(string itemCode) => armorItemDatabase.FindItemBy(itemCode);
}