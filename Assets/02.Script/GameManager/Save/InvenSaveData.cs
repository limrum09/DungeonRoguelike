using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvenSaveData
{
    public int inventoryCount;
    public List<int> slotIndexs = new List<int>();
    public List<int> itemCnts = new List<int>();
    public List<string> itemCodes = new List<string>();

    public List<string> weaponItemCode = new List<string>();
    public List<string> armorItemCode = new List<string>();
}
