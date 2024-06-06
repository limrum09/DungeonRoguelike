using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDatabase : MonoBehaviour
{
    public static SaveDatabase instance;

    public InvenSaveData invenSaveDatas = new InvenSaveData();

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
        SaveInven("SaveFile");
    }

    private void Start()
    {
        invenSlots = InvenData.instance.invenSlots;

        LoadInventory("SaveFile");

        // 데이터베이스가 올바르게 로드되었는지 확인
        Debug.Log($"InvenItemDatabase Loaded: {invenItemDatabase != null}");
        Debug.Log($"WeaponItemDatabase Loaded: {weaponItemDatabase != null}");
        Debug.Log($"ArmorItemDatabase Loaded: {armorItemDatabase != null}");
    }

    public void SaveInven(string fileName)
    {
        invenSaveDatas.inventoryCount = 0;
        invenSaveDatas.itemCodes.Clear();
        invenSaveDatas.itemCnts.Clear();
        invenSaveDatas.slotIndexs.Clear();

        invenSaveDatas.weaponItemCode.Clear();
        invenSaveDatas.armorItemCode.Clear();
        

        itemStatus = FindObjectOfType<ItemStatus>();

        for(int i =0; i < itemStatus.WeaponItems.Count; i++)
        {
            if(itemStatus.WeaponItems[i].weaponItem != null)
            {
                invenSaveDatas.weaponItemCode.Add(itemStatus.WeaponItems[i].weaponItem.ItemCode);
                Debug.Log("Save Item : " + itemStatus.WeaponItems[i].weaponItem.ItemCode);
            }            
        }

        for(int i = 0; i < itemStatus.ArmorItems.Count; i++)
        {
            if(itemStatus.ArmorItems[i].armorItem != null)
            {
                invenSaveDatas.armorItemCode.Add(itemStatus.ArmorItems[i].armorItem.ItemCode);
            }
        }

        invenSaveDatas.inventoryCount = invenSlots.Count;
        for (int i = 0; i < invenSaveDatas.inventoryCount; i++)
        {
            if(invenSlots[i] != null)
            {
                invenSaveDatas.itemCodes.Add(invenSlots[i].ItemCode);
                invenSaveDatas.itemCnts.Add(invenSlots[i].itemCnt);
                invenSaveDatas.slotIndexs.Add(i);
            }
        }

        Debug.Log("Inventory Lengh : " + invenSaveDatas.inventoryCount);

        // 데이터 저장
        string json = JsonUtility.ToJson(invenSaveDatas);   // Json 직열화

        // 파일 경로 설정
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // 파일에 Json 문자열 저장
        File.WriteAllText(path, json);
    }

    public void LoadInventory(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);

            invenSaveDatas = JsonUtility.FromJson<InvenSaveData>(loadJson);

            itemStatus = FindObjectOfType<ItemStatus>();


            for (int i = 0; i < invenSaveDatas.weaponItemCode.Count; i++)
            {
                if (!string.IsNullOrEmpty(invenSaveDatas.weaponItemCode[i]))
                {
                    WeaponItem item = SearchWeaponItem(invenSaveDatas.weaponItemCode[i]);
                    itemStatus.ChangeWeaponItem(item);
                    Debug.Log("Load Item : " + item);
                }                
            }

            for (int i = 0; i < invenSaveDatas.armorItemCode.Count; i++)
            {
                if (!string.IsNullOrEmpty(invenSaveDatas.armorItemCode[i]))
                {
                    ArmorItem item = SearchArmorItem(invenSaveDatas.armorItemCode[i]);
                    itemStatus.ChangeArmorItem(item);
                }
            }


            for (int i = 0; i < invenSaveDatas.slotIndexs.Count; i++)
            {
                
                if (!string.IsNullOrEmpty(invenSaveDatas.itemCodes[i]))
                {
                    InvenItem loadItem = null;
                    // Item DB에서 Code가 맞은 아이템을 찾아서 inventory의 해당 위치에 clone으로 만들어서 복사 후, 개수 추가
                    loadItem = SearchInvenItem(invenSaveDatas.itemCodes[i]).Clone();
                    loadItem.itemCnt = invenSaveDatas.itemCnts[i];
                    invenSlots[invenSaveDatas.slotIndexs[i]] = loadItem;
                    InvenData.instance.RefreshInvenSlot(invenSaveDatas.slotIndexs[i]);
                }
            }
        }
    }

    private InvenItem SearchInvenItem(string itemCode) => invenItemDatabase.FindItemBy(itemCode);
    private WeaponItem SearchWeaponItem(string itemCode) => weaponItemDatabase.FindItemBy(itemCode);
    private ArmorItem SearchArmorItem(string itemCode) => armorItemDatabase.FindItemBy(itemCode);
}