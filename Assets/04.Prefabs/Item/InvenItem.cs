using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ITEMTYPE
{
    All,
    POSTION,
    ETC
}

[CreateAssetMenu(fileName = "InvenItem", menuName = "Scriptable Object/Item")]
public class InvenItem : ScriptableObject
{
    public ITEMTYPE itemtype;
    public string itemName;
    public string itemInfo;
    public Sprite itemImage;
    public int itemCnt;
    public int amount;
    public bool isMax;

    public int hpHeal;
    public int mpHeal;
    public int increaseDamage;
    public float increaseSpeed;

    public float durationTime;

    public bool IsMax()
    {
        bool maxCheck = false;
        if(itemCnt >= amount)
        {
            maxCheck = true;
        }

        return maxCheck;
    }
}
