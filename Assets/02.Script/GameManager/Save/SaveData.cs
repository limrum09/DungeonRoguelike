using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int inventoryCount;
    public List<int> slotIndexs = new List<int>();
    public List<int> itemCnts = new List<int>();
    public List<string> itemCodes = new List<string>();

    public List<string> weaponItemCode = new List<string>();
    public List<string> armorItemCode = new List<string>();

    public PlayerStatusSaveData playerSaveStatus = new PlayerStatusSaveData();

    public List<QuestSaveData> activeQuestSaveData = new List<QuestSaveData>();
    public List<QuestSaveData> completedQuestSaveData = new List<QuestSaveData>();
}
