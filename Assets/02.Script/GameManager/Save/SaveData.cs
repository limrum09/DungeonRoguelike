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

    public int level;
    public int health;
    public int str;
    public int dex;
    public int luk;
    public int bonusState;

    public int currnetHP;

    public int exp;
    public int currentExp;
}
