using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BackEnd;

public class SaveDatabase : MonoBehaviour
{
    public SaveData saveData = new SaveData();

    private List<InvenItem> invenSlots;
    private ItemStatus itemStatus;
    private InputKey shortKey;

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

    private string gameDataRowInData = string.Empty;

    public void DataSaving()
    {
        SaveData("SaveFile", 10);
    }

    public void SaveDatabaseStart()
    {
        invenItemDatabase = Resources.Load<InvenItemDatabase>("InvenItemDatabase");
        weaponItemDatabase = Resources.Load<WeaponItemDatabase>("WeaponItemDatabase");
        armorItemDatabase = Resources.Load<ArmorItemDatabase>("ArmorItemDatabase");
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");

        QuestViewUI questViewUI = FindObjectOfType<QuestViewUI>();
        questViewUI.QuestUIStart();

        var manager = Manager.Instance;
        playerSaveStatus = manager.Game.PlayerCurrentStatus;
        invenSlots = manager.Game.InvenDatas.invenSlots;
        shortKey = manager.Key;

        LoadData("SaveFile");;
    }

    private void Initialzed()
    {
        saveData.invenSaveData.Clear();
        saveData.inventoryCount = 0;

        saveData.weaponItemCode.Clear();
        saveData.armorItemCode.Clear();

        saveData.playerSaveStatus = null;

        saveData.activeQuestSaveData.Clear();
        saveData.completedQuestSaveData.Clear();
        saveData.activeAchievementSavaData.Clear();
        saveData.completedAchievementSaveData.Clear();

        saveData.shortCutKeySaveData = null;
        saveData.shortCutButton.Clear();

        saveData.soundData.Clear();
    }

    public void SaveData(string fileName, int maxRepeatCount)
    {
        if (maxRepeatCount <= 0)
            return;

        Initialzed();

        saveData.playerSaveStatus = playerSaveStatus.GetPlayerSaveStatus();

        var questSystem = Manager.Instance.Quest;

        foreach (var activeQuest in questSystem.ActiveQeusts)
        {
            saveData.activeQuestSaveData.Add(activeQuest.QuestSave());
        }

        foreach(var completedQuest in questSystem.CompletedQuests)
        {
            saveData.completedQuestSaveData.Add(completedQuest.QuestSave());
        }

        foreach(var activeAcheivement in questSystem.ActiveAchievements)
        {
            saveData.activeAchievementSavaData.Add(activeAcheivement.QuestSave());
        }

        foreach (var completedAcheivement in questSystem.CompletedAchievements)
        {
            saveData.completedAchievementSaveData.Add(completedAcheivement.QuestSave());
        }

        itemStatus = FindObjectOfType<ItemStatus>();

        // 무기 저장
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

        // 방어구 저장
        for(int i = 0; i < itemStatus.ArmorItems.Count; i++)
        {
            // 아이템을 장착하지 않는 경우 itemStatus.ArmorItmes[i].armorItem의 값은 null값이다.
            if(itemStatus.ArmorItems[i].armorItem != null)
            {
                Debug.Log("Save Item : " + itemStatus.ArmorItems[i].armorItem.ItemCode);
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
                InventorySaveData newData = new InventorySaveData();
                newData.itemnCnt = invenSlots[i].itemCnt;
                newData.itemCode = invenSlots[i].ItemCode;
                newData.index = i;

                saveData.invenSaveData.Add(newData);
            }
        }

        // 단축키 저장
        saveData.shortCutKeySaveData = shortKey.SerializeShortCutKeyDictionary();

        ShortKeyManager shortCutKeys = Manager.Instance.UIAndScene.ShortCutBox;
        // 단축키 저장
        for (int i = 0;i < Manager.Instance.UIAndScene.ShortCutBox.ShortKeys.Count; i++)
        {
            ShortCutKeySaveData shortCutKeySaveData = new ShortCutKeySaveData();

            // 단축키에 아이템이 올려져 있는 경우
            if (shortCutKeys.ShortKeys[i].GetItem() != null)
            {
                shortCutKeySaveData.isItem = true;
                shortCutKeySaveData.itemIndex = shortCutKeys.ShortKeys[i].ItemIndex;
            }
            // 단축키에 스킬이 올려져 있는 경우
            else if (shortCutKeys.ShortKeys[i].GetSkill() != null)
            {
                shortCutKeySaveData.isItem = false;
                shortCutKeySaveData.skill = shortCutKeys.ShortKeys[i].GetSkill();
            }
            //단축키에 아무 것도 없는 경우
            else
                shortCutKeySaveData = null;

            // 값 추가
            saveData.shortCutButton.Add(shortCutKeySaveData);
        }

        // SoundData 저장
        for (int i = 0; i < Manager.Instance.Sound.SoundSliders.Count; i++)
        {
            saveData.soundData.Add(Manager.Instance.Sound.SoundSliders[i].SoundValueSave());
        }

        if (Manager.Instance.UIAndScene.LoddingUI.gameObject.activeSelf)
            Manager.Instance.UIAndScene.LoddingUI.LoddingRateValue("유저 정보 확인 중...!", 25);

        // 데이터 저장
        string json = JsonUtility.ToJson(saveData);   // Json 직열화

        // 파일 경로 설정
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // 파일에 Json 문자열 저장
        File.WriteAllText(path, json);

        Param param = new Param();

        param.Add("JsonSaveData", json);
        param.Add("Level", Manager.Instance.Game.PlayerCurrentStatus.Level);

        BackendReturnObject bro = Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where());

        switch (BackendManager.Instance.CheckError(bro))
        {
            case "401":
                Debug.LogError("서버 점검 중");
                break;
            case "False":
            case "false":
                Debug.LogError("초기화 실패");
                break;
            case "Repeat":
            case "repeat":
                Debug.LogError("연결 재시도");
                SaveData(fileName, maxRepeatCount - 1);
                break;
            case "Success":
            case "success":
                // 서버 DB에 정보가 존재할 경우
                if (bro.GetReturnValuetoJSON() != null)
                {
                    // 서버 DB에는 존재하나 그 속에는 데이터량이 0줄인 경우
                    if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
                    {
                        Debug.Log("DB서버에 데이터가 있지만 데이터 량이 0줄이기에, 자신의 데이터를 새로이 추가한다");
                        // 서버 DB에 데이터를 추가한다.
                        // 이게 아니라 위에 있는거 해야함
                        BackendManager.Instance.InsertGameData(Constants.USER_DATA_TABLE, 10, param);
                    }
                    else
                    {
                        Debug.Log("DB서버에 데이터가 있어서, 자신의 데이터를 갱신한다");
                        // 서버 DB에 있는 자신의 데이터를 갱신한다.
                        BackendManager.Instance.UpdateGameData(Constants.USER_DATA_TABLE, bro.GetInDate(), param);
                    }
                }
                // 서버 DB에 정보가 존재하지 않을 경우
                else
                {
                    Debug.Log("DB서버에 데이터가 없어, 서버에 자신의 데이터를 새로이 추가한다");
                    // 서버 DB에 자신의 데이터를 추가한다.
                    BackendManager.Instance.InsertGameData(Constants.USER_DATA_TABLE, 10, param);
                }
                break;
        }
    }


    public void LoadData(string fileName)
    {
        Debug.Log("Load Data");

        string path = Path.Combine(Application.persistentDataPath, fileName);

        bool getSaveDataToBackendServer = false;

        shortKey.DeserializeShortCutKeyDictionary(saveData.shortCutKeySaveData);

        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
        {
            if(callback.IsSuccess())
            {
                Manager.Instance.UIAndScene.Notion.SetNotionText("게임 데이터 불러오기 완료");
                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if(gameDataJson.Count <= 0)
                    {
                        Manager.Instance.UIAndScene.Notion.SetNotionText("데이터가 존재하지 않습니다");
                    }
                    else
                    {
                        string loadJson = gameDataJson[0]["JsonSaveData"].ToString();

                        // 해당 위치에 InvenSaveData가 변환된 데이터가 있으면 가져오기
                        saveData = JsonUtility.FromJson<SaveData>(loadJson);

                        LoadDatas();

                        Manager.Instance.UIAndScene.Notion.SetNotionText("뒤끝 서버에서 데이터 불러오기 완료");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log("Json 데이터 피팅 실패 : " + e);

                    if (File.Exists(path) && !getSaveDataToBackendServer)
                    {
                        // Json문자열을 읽어오기
                        string loadJson = File.ReadAllText(path);

                        // 해당 위치에 InvenSaveData가 변환된 데이터가 있으면 가져오기
                        saveData = JsonUtility.FromJson<SaveData>(loadJson);

                        Manager.Instance.UIAndScene.Notion.SetNotionText("로컬에서 가져오기");

                        LoadDatas();
                    }
                }
            }
        });
    }

    private void LoadDatas()
    {
        // saveData = JsonUtility.FromJson<SaveData>(json);
        var manager = Manager.Instance;

        foreach (var activeQuest in saveData.activeQuestSaveData)
        {
            Quest newActiveQuest = questDatabase.FindQuestBy(activeQuest.questCode);
            manager.Quest.LoadActiveQuest(activeQuest, newActiveQuest);
        }

        foreach (var completedQuest in saveData.completedQuestSaveData)
        {
            Quest newCompletedQuest = questDatabase.FindQuestBy(completedQuest.questCode);
            manager.Quest.LoadCompletedQuest(completedQuest, newCompletedQuest);
        }

        foreach (var activeAchivement in saveData.activeAchievementSavaData)
        {
            Achievement newActiveAchievement = achievementDatabase.FindQuestBy(activeAchivement.questCode) as Achievement;
            manager.Quest.LoadActiveQuest(activeAchivement, newActiveAchievement);
        }

        foreach (var completedAchivement in saveData.completedAchievementSaveData)
        {
            Achievement newCompletedAchievement = achievementDatabase.FindQuestBy(completedAchivement.questCode) as Achievement;
            manager.Quest.LoadCompletedQuest(completedAchivement, newCompletedAchievement);
        }


        for (int i = 0; i < saveData.weaponItemCode.Count; i++)
        {
            if (!string.IsNullOrEmpty(saveData.weaponItemCode[i]))
            {
                // ItemCode가 일치하는 무기 찾기
                WeaponItem item = SearchWeaponItem(saveData.weaponItemCode[i]);

                // GameManager의 PlayerWeaponChange 호출
                manager.Game.PlayerWeaponChange(item);
            }
        }

        for (int i = 0; i < saveData.armorItemCode.Count; i++)
        {
            if (!string.IsNullOrEmpty(saveData.armorItemCode[i]))
            {
                // ItemCode가 일치하는 방어구 찾기
                ArmorItem item = SearchArmorItem(saveData.armorItemCode[i]);

                // GameManager의 PlayerArmorChange 호출
                manager.Game.PlayerArmorChange(item);
            }
        }


        // 인벤토리 초가화
        InvenData.instance.Initialzed(saveData.inventoryCount);
        for (int i = 0; i < saveData.invenSaveData.Count; i++)
        {
            if (!string.IsNullOrEmpty(saveData.invenSaveData[i].itemCode))
            {
                InvenItem loadItem = null;
                // Item DB에서 Code가 맞은 아이템을 찾아 clone으로 복사
                loadItem = SearchInvenItem(saveData.invenSaveData[i].itemCode).Clone();
                // 찾은 Item의 개수 변경
                loadItem.itemCnt = saveData.invenSaveData[i].itemnCnt;
                // 저장되어 있는 index를 가져와서 invenSlots의 index 추가
                invenSlots[i] = loadItem;

                // 해당 추가된 아이템 슬롯을 새로고침
                InvenData.instance.RefreshInvenSlot(saveData.invenSaveData[i].index);
            }
        }

        playerSaveStatus.SetPlayerSavestatus(saveData.playerSaveStatus);

        // 단축키 로드
        shortKey.DeserializeShortCutKeyDictionary(saveData.shortCutKeySaveData);        

        // UI에 보여지는 단축키 값 로드
        manager.UIAndScene.LoadShortCutKeys(saveData.shortCutButton);
        manager.UIAndScene.LoadSettingUIData();

        for (int i = 0; i < saveData.soundData.Count; i++)
        {
            manager.Sound.SetSoundVolumeValueToLoad(saveData.soundData[i].masterType, saveData.soundData[i].audioType, saveData.soundData[i].value);
        }

        manager.UIAndScene.AchievementUI.AchievementUIStart();

        manager.UIAndScene.LoddingUI.LoddingRateValue("유저 정보 확인 중...!", 40);

        manager.canUseShortcutKey = true;
        Debug.Log("사용자 데이터 로드 완료");
    }

    private InvenItem SearchInvenItem(string itemCode) => invenItemDatabase.FindItemBy(itemCode);
    private WeaponItem SearchWeaponItem(string itemCode) => weaponItemDatabase.FindItemBy(itemCode);
    private ArmorItem SearchArmorItem(string itemCode) => armorItemDatabase.FindItemBy(itemCode);
}