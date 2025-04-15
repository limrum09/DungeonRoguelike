using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int inventoryCount;
    public int invenGlodCount;
    public List<InventorySaveData> invenSaveData = new List<InventorySaveData>();

    public List<string> weaponItemCode = new List<string>();
    public List<string> armorItemCode = new List<string>();

    public PlayerStatusSaveData playerSaveStatus = new PlayerStatusSaveData();

    public List<QuestSaveData> activeQuestSaveData = new List<QuestSaveData>();
    public List<QuestSaveData> completedQuestSaveData = new List<QuestSaveData>();
    public List<QuestSaveData> activeAchievementSavaData = new List<QuestSaveData>();
    public List<QuestSaveData> completedAchievementSaveData = new List<QuestSaveData>();

    public string shortCutKeySaveData;
    public List<ShortCutKeySaveData> shortCutButton = new List<ShortCutKeySaveData>();
    public List<SoundSliderData> soundData = new List<SoundSliderData>();

    public int skillPoint;
    public List<SkillData> skillData = new List<SkillData>();
}
