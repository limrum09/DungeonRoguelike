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

    [Header("Player Data")]
    [SerializeField]
    private PlayerStatus playerSaveStatus;

    [Header("DataBase")]
    [SerializeField]
    private InvenItemDatabase invenItemDatabase;
    [SerializeField]
    private WeaponItemDatabase weaponItemDatabase;
    [SerializeField]
    private ArmorItemDatabase armorItemDatabase;
    [SerializeField]
    private QuestDatabase questDatabase;
    [SerializeField]
    private QuestDatabase achievementDatabase;
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
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");
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

        saveData.playerSaveStatus = null;

        saveData.activeQuestSaveData.Clear();
        saveData.completedQuestSaveData.Clear();
    }

    public void SaveData(string fileName)
    {
        Initialzed();

        saveData.playerSaveStatus = playerSaveStatus.GetPlayerSaveStatus();

        var questSystem = QuestSystem.instance;

        foreach (var activeQuest in questSystem.ActiveQeusts)
        {
            saveData.activeQuestSaveData.Add(activeQuest.QuestSave());
        }

        foreach(var completedQuest in questSystem.CompletedQuests)
        {
            saveData.completedQuestSaveData.Add(completedQuest.QuestSave());
        }


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

            foreach(var activeQuest in saveData.activeQuestSaveData)
            {
                Quest newActiveQuest = questDatabase.FindQuestBy(activeQuest.questCode);
                QuestSystem.instance.LoadActiveQuest(activeQuest, newActiveQuest);
            }

            foreach (var completedQuest in saveData.completedQuestSaveData)
            {
                Quest newCompletedQuest = questDatabase.FindQuestBy(completedQuest.questCode);
                QuestSystem.instance.LoadCompletedQuest(completedQuest, newCompletedQuest);
            }

            

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

            playerSaveStatus.SetPlayerSavestatus(saveData.playerSaveStatus);

            QuestViewUI questViewUI = FindObjectOfType<QuestViewUI>();
            questViewUI.QuestUIStart();
        }
    }

    private InvenItem SearchInvenItem(string itemCode) => invenItemDatabase.FindItemBy(itemCode);
    private WeaponItem SearchWeaponItem(string itemCode) => weaponItemDatabase.FindItemBy(itemCode);
    private ArmorItem SearchArmorItem(string itemCode) => armorItemDatabase.FindItemBy(itemCode);
}