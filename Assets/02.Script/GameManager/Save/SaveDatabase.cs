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
            // itemStatus.WeaponItems는 왼손과 오른손을 위해 배열의 크기가 2이다. 
            // 무기가 기본적으로 오른손에 장착하며 한개의 무기를 사용하면 오른손에 장착된다.
            // 왼손에 장착된 무기가 없는 경우 itemStatus.WeaponItems[i].weaponItem는 null값을 가진다.
            if(itemStatus.WeaponItems[i].weaponItem != null)
            {
                saveData.weaponItemCode.Add(itemStatus.WeaponItems[i].weaponItem.ItemCode);
            }            
        }

        for(int i = 0; i < itemStatus.ArmorItems.Count; i++)
        {
            // 아이템을 장착하지 않는 경우 itemStatus.ArmorItmes[i].armorItem의 값은 null값이다.
            if(itemStatus.ArmorItems[i].armorItem != null)
            {
                saveData.armorItemCode.Add(itemStatus.ArmorItems[i].armorItem.ItemCode);
            }
        }

        // 현제 인벤토리의 크기를 저장
        saveData.inventoryCount = invenSlots.Count;
        for (int i = 0; i < saveData.inventoryCount; i++)
        {
            if(invenSlots[i] != null)
            {
                // 저장되는 ItemCode, itemCnt, index는 서로 다른 배열에 값을 저장된다.
                // 서로 다른 배열의 같은 index값을 가진다. (저장시 i의 값에 저장된다.)
                saveData.itemCodes.Add(invenSlots[i].ItemCode);
                saveData.itemCnts.Add(invenSlots[i].itemCnt);
                saveData.slotIndexs.Add(i);
            }
        }

        // 데이터 저장
        string json = JsonUtility.ToJson(saveData);   // Json 직열화

        // 파일 경로 설정
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // 파일에 Json 문자열 저장
        File.WriteAllText(path, json);
    }


    public void LoadData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            // Json문자열을 읽어오기
            string loadJson = File.ReadAllText(path);

            // 해당 위치에 InvenSaveData가 변환된 데이터가 있으면 가져오기
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
                    // ItemCode가 일치하는 무기 찾기
                    WeaponItem item = SearchWeaponItem(saveData.weaponItemCode[i]);

                    // GameManager의 PlayerWeaponChange 호출
                    GameManager.instance.PlayerWeaponChange(item);
                }                
            }

            for (int i = 0; i < saveData.armorItemCode.Count; i++)
            {
                if (!string.IsNullOrEmpty(saveData.armorItemCode[i]))
                {
                    // ItemCode가 일치하는 방어구 찾기
                    ArmorItem item = SearchArmorItem(saveData.armorItemCode[i]);

                    // GameManager의 PlayerArmorChange 호출
                    GameManager.instance.PlayerArmorChange(item);
                }
            }


            // 인벤토리 초가화
            InvenData.instance.Initialzed(saveData.inventoryCount);
            for (int i = 0; i < saveData.slotIndexs.Count; i++)
            {   
                if (!string.IsNullOrEmpty(saveData.itemCodes[i]))
                {
                    InvenItem loadItem = null;
                    // Item DB에서 Code가 맞은 아이템을 찾아 clone으로 복사
                    loadItem = SearchInvenItem(saveData.itemCodes[i]).Clone();
                    // 찾은 Item의 개수 변경
                    loadItem.itemCnt = saveData.itemCnts[i];
                    // 저장되어 있는 index를 가져와서 invenSlots의 index 추가
                    invenSlots[saveData.slotIndexs[i]] = loadItem;
                    // 해당 추가된 아이템 슬롯을 새로고침
                    InvenData.instance.RefreshInvenSlot(saveData.slotIndexs[i]);
                }
            }   
        }
    }

    private InvenItem SearchInvenItem(string itemCode) => invenItemDatabase.FindItemBy(itemCode);
    private WeaponItem SearchWeaponItem(string itemCode) => weaponItemDatabase.FindItemBy(itemCode);
    private ArmorItem SearchArmorItem(string itemCode) => armorItemDatabase.FindItemBy(itemCode);
}