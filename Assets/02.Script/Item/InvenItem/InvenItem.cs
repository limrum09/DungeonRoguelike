using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ITEMTYPE
{
    All,
    POTION,
    ETC
}

[CreateAssetMenu(fileName = "InvenItem", menuName = "Scriptable Object/Item")]
public class InvenItem : ScriptableObject
{
    public ITEMTYPE itemtype;
    [SerializeField]
    private string itemCode;
    public string itemName;
    public string itemInfo;
    public Sprite itemImage;
    public int itemCnt;
    [SerializeField]
    private int amount;
    public bool isMax;

    public int hpHeal;
    public int mpHeal;
    public int increaseDamage;
    public float increaseSpeed;

    public float durationTime;

    public string ItemCode => itemCode;
    public bool IsMax() => itemCnt >= amount;
    public int ItemAmount => amount;

    public InvenItem Clone()
    {
        var clone = ScriptableObject.CreateInstance<InvenItem>();

        clone.itemtype = this.itemtype;

        clone.itemCode = this.itemCode;
        clone.itemName = this.itemName;
        clone.itemInfo = this.itemInfo;
        clone.itemImage = this.itemImage;
        clone.itemCnt = this.itemCnt;
        clone.amount = this.amount;
        clone.isMax = this.isMax;

        clone.hpHeal = this.hpHeal;
        clone.mpHeal = this.mpHeal;
        clone.increaseDamage = this.increaseDamage;
        clone.increaseSpeed = this.increaseSpeed;

        clone.durationTime = this.durationTime;

        return clone;
    }
}
