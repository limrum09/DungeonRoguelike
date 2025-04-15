using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using BackEnd;

[System.Flags]
public enum SaveAndLoadOptions { 
    None = 0,
    Quest = 1 << 0,
    Equipment = 1 << 1,
    Status = 1 << 2,
    Inventory = 1 << 3,
    ShortCutKey = 1 << 4,
    SoundSetting = 1 << 5,
    Skill = 1 << 6,
    All = Quest | Equipment | Status | Inventory | ShortCutKey | SoundSetting | Skill
}

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
    private AchievementDatabase achievementDatabase;
    [SerializeField]
    private SkillDatabase skillDatabase;

    public void DataSaving(SaveAndLoadOptions saveOption)
    {
        SaveData(10, saveOption);
    }

    public void SaveDatabaseStart()
    {
        invenItemDatabase = Resources.Load<InvenItemDatabase>("InvenItemDatabase");
        weaponItemDatabase = Resources.Load<WeaponItemDatabase>("WeaponItemDatabase");
        armorItemDatabase = Resources.Load<ArmorItemDatabase>("ArmorItemDatabase");
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<AchievementDatabase>("AchievementDatabase");
        skillDatabase = Resources.Load<SkillDatabase>("SkillDatabase");

        QuestViewUI questViewUI = FindObjectOfType<QuestViewUI>();
        questViewUI.QuestUIStart();

        var manager = Manager.Instance;
        playerSaveStatus = manager.Game.PlayerCurrentStatus;
        invenSlots = manager.Game.InvenDatas.invenSlots;
        shortKey = manager.Key;

        LoadData(SaveAndLoadOptions.All);
    }

    // 전체 값 초기화
    public void Initialzed()
    {
        saveData.invenSaveData.Clear();
        saveData.inventoryCount = 0;

        saveData.weaponItemCode.Clear();
        saveData.armorItemCode.Clear();

        saveData.playerSaveStatus.Reset();

        saveData.activeQuestSaveData.Clear();
        saveData.completedQuestSaveData.Clear();
        saveData.activeAchievementSavaData.Clear();
        saveData.completedAchievementSaveData.Clear();

        saveData.shortCutKeySaveData = null;
        saveData.shortCutButton.Clear();

        saveData.soundData.Clear();
        saveData.skillData.Clear();
    }

    #region Save Functions
    private void SaveQusetAndAchievement()
    {
        // 값 초기화
        saveData.activeQuestSaveData.Clear();
        saveData.completedQuestSaveData.Clear();
        saveData.activeAchievementSavaData.Clear();
        saveData.completedAchievementSaveData.Clear();

        var questSystem = Manager.Instance.Quest;

        foreach (var activeQuest in questSystem.ActiveQeusts)
        {
            saveData.activeQuestSaveData.Add(activeQuest.QuestSave());
        }

        foreach (var completedQuest in questSystem.CompletedQuests)
        {
            saveData.completedQuestSaveData.Add(completedQuest.QuestSave());
        }

        foreach (var activeAcheivement in questSystem.ActiveAchievements)
        {
            saveData.activeAchievementSavaData.Add(activeAcheivement.QuestSave());
        }

        foreach (var completedAcheivement in questSystem.CompletedAchievements)
        {
            saveData.completedAchievementSaveData.Add(completedAcheivement.QuestSave());
        }
    }

    private void SaveWeaponAndArmor()
    {
        // 값 초기화
        saveData.weaponItemCode.Clear();
        saveData.armorItemCode.Clear();

        itemStatus = FindObjectOfType<ItemStatus>();

        // 무기 저장
        for (int i = 0; i < itemStatus.WeaponItems.Count; i++)
        {
            // itemStatus.WeaponItems는 왼손과 오른손을 위해 배열의 크기가 2이다. 
            // 무기가 기본적으로 오른손에 장착하며 한개의 무기를 사용하면 오른손에 장착된다.
            // 왼손에 장착된 무기가 없는 경우 itemStatus.WeaponItems[i].weaponItem는 null값을 가진다.
            if (itemStatus.WeaponItems[i].weaponItem != null)
            {
                saveData.weaponItemCode.Add(itemStatus.WeaponItems[i].weaponItem.ItemCode);
            }
        }

        // 방어구 저장
        for (int i = 0; i < itemStatus.ArmorItems.Count; i++)
        {
            // 아이템을 장착하지 않는 경우 itemStatus.ArmorItmes[i].armorItem의 값은 null값이다.
            if (itemStatus.ArmorItems[i].armorItem != null)
            {
                saveData.armorItemCode.Add(itemStatus.ArmorItems[i].armorItem.ItemCode);
            }
        }
    }

    private void SavePlayerStatus()
    {
        // 값 초기화
        saveData.playerSaveStatus.Reset();

        saveData.playerSaveStatus = playerSaveStatus.GetPlayerSaveStatus();
    }

    private void SaveInventory()
    {
        // 값 초기화
        saveData.invenSaveData.Clear();
        saveData.inventoryCount = 0;

        // 현제 인벤토리의 크기를 저장
        saveData.inventoryCount = invenSlots.Count;
        saveData.invenGlodCount = InvenData.instance.InvenGoldCoinCount;
        for (int i = 0; i < saveData.inventoryCount; i++)
        {
            if (invenSlots[i] != null)
            {
                // 저장되는 ItemCode, itemCnt, index는 서로 다른 배열에 값을 저장된다.
                // 서로 다른 배열의 같은 index값을 가진다. (저장시 i의 값에 저장된다.)
                InventorySaveData newData = new InventorySaveData();
                newData.itemnCnt = invenSlots[i].ItemCnt;
                newData.itemCode = invenSlots[i].ItemCode;
                newData.index = i;

                saveData.invenSaveData.Add(newData);
            }
        }
    }

    private void SaveShortCutKey()
    {
        // 값 초기화
        saveData.shortCutKeySaveData = null;
        saveData.shortCutButton.Clear();

        // 8개의 단축키 슬롯 값 저장
        saveData.shortCutKeySaveData = shortKey.SerializeShortCutKeyDictionary();

        ShortKeyManager shortCutKeys = Manager.Instance.UIAndScene.ShortCutBox;
        // 단축키 슬롯의 아이템 및 스킬 저장
        for (int i = 0; i < Manager.Instance.UIAndScene.ShortCutBox.ShortKeys.Count; i++)
        {
            ShortCutKeySaveData shortCutKeySaveData = new ShortCutKeySaveData();

            shortCutKeySaveData.itemIndex = -1;
            shortCutKeySaveData.isItem = false;
            shortCutKeySaveData.skill = null;

            // 단축키에 아이템이올려져 있는 경우
            if (shortCutKeys.ShortKeys[i].GetItem() != null)
            {
                shortCutKeySaveData.itemIndex = shortCutKeys.ShortKeys[i].ItemIndex;
                shortCutKeySaveData.isItem = true;
            }
            // 스킬인 경우
            else if (shortCutKeys.ShortKeys[i].GetSkill() != null)
            {
                shortCutKeySaveData.skill = shortCutKeys.ShortKeys[i].GetSkill();
            }

            // 값 추가
            saveData.shortCutButton.Add(shortCutKeySaveData);
        }
    }

    private void SaveSoundSetting()
    {
        // 값 초기화
        saveData.soundData.Clear();

        // SoundData 저장
        for (int i = 0; i < Manager.Instance.Sound.SoundSliders.Count; i++)
        {
            saveData.soundData.Add(Manager.Instance.Sound.SoundSliders[i].SoundValueSave());
        }
    }

    private void SaveSkillDatas()
    {
        List<string> tempSaveSkillCode = new List<string>();

        foreach(var skill in skillDatabase.Datas)
        {
            // skill code가 빈 값이면 저장안함
            if (string.IsNullOrEmpty(skill.SkillCode))
            {
                continue;
            }   

            // 겹치는 것이 있는지 검사
            if (tempSaveSkillCode.Contains(skill.SkillCode))
                continue;

            tempSaveSkillCode.Add(skill.SkillCode);
            saveData.skillData.Add(skill.SaveSkillData());
        }
    }
    #endregion

    #region Backend Save
    private void SaveData(int maxRepeatCount, SaveAndLoadOptions saveOption)
    {
        if (maxRepeatCount <= 0)
            return;

        if (saveOption == SaveAndLoadOptions.None)
            return;

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.Quest))
            SaveQusetAndAchievement();

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.Equipment))
            SaveWeaponAndArmor();

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.Status))
            SavePlayerStatus();

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.Inventory))
            SaveInventory();

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.ShortCutKey))
            SaveShortCutKey();

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.SoundSetting))
            SaveSoundSetting();

        if (saveOption.HasFlag(SaveAndLoadOptions.All) || saveOption.HasFlag(SaveAndLoadOptions.Skill))
            SaveSkillDatas();

        if (Manager.Instance.UIAndScene.LoddingUI.gameObject.activeSelf)
            Manager.Instance.UIAndScene.LoddingUI.LoddingRateValue("유저 정보 확인 중...!", 25);

        // 데이터 저장
        string json = JsonUtility.ToJson(saveData);   // Json 직열화

        // 파일 경로 설정
        string path = Path.Combine(Application.persistentDataPath, UserInfo.UserData.gamerId);

        // 파일에 Json 문자열 저장
        File.WriteAllText(path, json);

        Param param = new Param();

        param.Add("JsonSaveData", json);
        param.Add("Level", Manager.Instance.Game.Level);

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
                SaveData(maxRepeatCount - 1, saveOption);
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
    #endregion

    #region Load Functions
    private void LoadQuestsAndAchievements()
    {
        var quest = Manager.Instance.Quest;

        foreach (var activeQuest in saveData.activeQuestSaveData)
        {
            Quest newActiveQuest = questDatabase.FindByCode(activeQuest.questCode);
            quest.LoadActiveQuest(activeQuest, newActiveQuest);
        }

        foreach (var completedQuest in saveData.completedQuestSaveData)
        {
            Quest newCompletedQuest = questDatabase.FindByCode(completedQuest.questCode);
            quest.LoadCompletedQuest(completedQuest, newCompletedQuest);
        }

        foreach (var activeAchivement in saveData.activeAchievementSavaData)
        {
            Achievement newActiveAchievement = achievementDatabase.FindByCode(activeAchivement.questCode) as Achievement;
            quest.LoadActiveQuest(activeAchivement, newActiveAchievement);
        }

        foreach (var completedAchivement in saveData.completedAchievementSaveData)
        {
            Achievement newCompletedAchievement = achievementDatabase.FindByCode(completedAchivement.questCode) as Achievement;
            quest.LoadCompletedQuest(completedAchivement, newCompletedAchievement);
        }

        Manager.Instance.UIAndScene.AchievementUI.AchievementUIStart();
    }

    private void LoadWeaponAndArmor()
    {
        for (int i = 0; i < saveData.weaponItemCode.Count; i++)
        {
            if (!string.IsNullOrEmpty(saveData.weaponItemCode[i]))
            {
                // ItemCode가 일치하는 무기 찾기
                WeaponItem item = SearchWeaponItem(saveData.weaponItemCode[i]);

                // GameManager의 PlayerWeaponChange 호출
                Manager.Instance.Game.PlayerWeaponChange(item);
            }
        }

        for (int i = 0; i < saveData.armorItemCode.Count; i++)
        {
            if (!string.IsNullOrEmpty(saveData.armorItemCode[i]))
            {
                // ItemCode가 일치하는 방어구 찾기
                ArmorItem item = SearchArmorItem(saveData.armorItemCode[i]);

                // GameManager의 PlayerArmorChange 호출
                Manager.Instance.Game.PlayerArmorChange(item);
            }
        }
    }

    private void LoadPlayerStatus()
    {
        playerSaveStatus.SetPlayerSavestatus(saveData.playerSaveStatus);
    }

    private void LoadInventory()
    {
        // 인벤토리 초가화
        InvenData.instance.Initialzed(saveData.inventoryCount);
        InvenData.instance.ChangeGlodValue(saveData.invenGlodCount);
        for (int i = 0; i < saveData.invenSaveData.Count; i++)
        {
            if (!string.IsNullOrEmpty(saveData.invenSaveData[i].itemCode))
            {
                InvenItem loadItem = null;
                // Item DB에서 Code가 맞은 아이템을 찾아 clone으로 복사
                loadItem = SearchInvenItem(saveData.invenSaveData[i].itemCode).Clone();
                // 찾은 Item의 개수 변경
                loadItem.SetItemCount(saveData.invenSaveData[i].itemnCnt);
                // 저장되어 있는 index를 가져와서 invenSlots의 index 추가
                invenSlots[i] = loadItem;

                // 해당 추가된 아이템 슬롯을 새로고침
                InvenData.instance.RefreshInvenSlot(saveData.invenSaveData[i].index);
            }
        }
    }

    private void LoadShortCutKey()
    {
        // 단축키 로드
        shortKey.DeserializeShortCutKeyDictionary(saveData.shortCutKeySaveData);

        // UI에 보여지는 단축키 값 로드
        Manager.Instance.UIAndScene.LoadShortCutKeys(saveData.shortCutButton);
        Manager.Instance.UIAndScene.LoadSettingUIData();
    }

    private void LoadSoundSetting()
    {
        // 소리값 로드
        for (int i = 0; i < saveData.soundData.Count; i++)
        {
            Manager.Instance.Sound.SetSoundVolumeValueToLoad(saveData.soundData[i].masterType, saveData.soundData[i].audioType, saveData.soundData[i].value);
        }
    }

    private void LoadSkillDatas()
    {
        skillDatabase.ResetSkillData();

        for (int i = 0; i < saveData.skillData.Count; i++)
        {
            ActiveSkill findSkill = skillDatabase.FindByCode(saveData.skillData[i].skillCode);

            if(findSkill != null)
            {
                findSkill.LoadSkillData(saveData.skillData[i]);
            }
        }

        Manager.Instance.Skill.CheckSkillConditionToPlayerLevelUp();
    }

    private void LocalLoadDatas()
    {
        string path = Path.Combine(Application.persistentDataPath, UserInfo.UserData.gamerId);

        if (File.Exists(path))
        {
            // Json문자열을 읽어오기
            string loadJson = File.ReadAllText(path);

            // 해당 위치에 InvenSaveData가 변환된 데이터가 있으면 가져오기
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            LoadDatas();
        }
    }

    private void LoadDatas()
    {
        // saveData = JsonUtility.FromJson<SaveData>(json);
        LoadQuestsAndAchievements();
        LoadWeaponAndArmor();
        LoadPlayerStatus();
        LoadInventory();
        LoadShortCutKey();
        LoadSoundSetting();
        LoadSkillDatas();
    }
    #endregion

    #region Backend Load
    public void LoadData(SaveAndLoadOptions loadOption)
    {
        Debug.Log("Load Data");

        if (loadOption == SaveAndLoadOptions.None)
            return;

        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
        {
            var notion = Manager.Instance.UIAndScene.Notion;
            if (callback.IsSuccess())
            {
                notion.SetNotionText("게임 데이터 불러오기 완료");
                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    if (gameDataJson.Count <= 0)
                    {
                        notion.SetNotionText("데이터가 존재하지 않습니다");
                    }
                    else
                    {
                        string loadJson = gameDataJson[0]["JsonSaveData"].ToString();

                        // 해당 위치에 InvenSaveData가 변환된 데이터가 있으면 가져오기
                        saveData = JsonUtility.FromJson<SaveData>(loadJson);
                        
                        if (loadOption.HasFlag(SaveAndLoadOptions.All))
                        {
                            LoadDatas();
                        }
                        else
                        {
                            if (loadOption.HasFlag(SaveAndLoadOptions.Quest))
                                LoadQuestsAndAchievements();

                            if (loadOption.HasFlag(SaveAndLoadOptions.Equipment))
                                LoadWeaponAndArmor();

                            if (loadOption.HasFlag(SaveAndLoadOptions.Inventory))
                                LoadInventory();

                            if (loadOption.HasFlag(SaveAndLoadOptions.Status))
                                LoadPlayerStatus();

                            if (loadOption.HasFlag(SaveAndLoadOptions.ShortCutKey))
                                LoadShortCutKey();

                            if (loadOption.HasFlag(SaveAndLoadOptions.SoundSetting))
                                LoadSoundSetting();

                            if (loadOption.HasFlag(SaveAndLoadOptions.Skill))
                                LoadSkillDatas();
                        }

                        Manager.Instance.canInputKey = true;
                        notion.SetNotionText("뒤끝 서버에서 데이터 불러오기 완료");
                    }
                }
                // 무언가 오류가 나올 경우
                catch (System.Exception e)
                {
                    Debug.Log("Json 데이터 피팅 실패 : " + e);

                    LocalLoadDatas();
                    notion.SetNotionText("로컬에서 가져오기");
                }

                Manager.Instance.UIAndScene.LoddingUI.LoddingRateValue("유저 정보 확인 중...!", 40);
            }
            // 로그인 정보 불러오기 실패
            else
            {
                notion.SetNotionText("로그인 정보 불러오기 실패 : " + callback);
                LocalLoadDatas();
                notion.SetNotionText("로컬에서 가져오기");
            }
        });
    }
    #endregion

    private InvenItem SearchInvenItem(string itemCode) => invenItemDatabase.FindByCode(itemCode);
    private WeaponItem SearchWeaponItem(string itemCode) => weaponItemDatabase.FindByCode(itemCode);
    private ArmorItem SearchArmorItem(string itemCode) => armorItemDatabase.FindByCode(itemCode);
}