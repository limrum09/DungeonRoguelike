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
    [Header("Info")]
    public ITEMTYPE itemtype;
    [SerializeField]
    private string itemCode;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemInfo;
    [SerializeField]
    private Sprite itemImage;
    [SerializeField]
    private int itemCnt;
    [SerializeField]
    private int amount;
    [SerializeField]
    private bool isMax;

    [Header("HP & MP")]
    [SerializeField]
    private bool isSustain;
    [SerializeField]
    private float healSpeedTime;
    [SerializeField]
    private int hpHeal;
    [SerializeField]
    private int mpHeal;

    [Header("Buff")]
    [SerializeField]
    private int increaseDamage;
    [SerializeField]
    private float increaseSpeed;
    [SerializeField]
    private float durationTime;

    public string ItemCode => itemCode;
    public string ItemName => itemName;
    public string ItemInfo => itemInfo;
    public Sprite ItemImage => itemImage;
    public int ItemCnt => itemCnt;
    public bool IsMax() => itemCnt >= amount;
    public int ItemAmount => amount;

    public bool IsSustain => isSustain;
    public float HealSpeedTime => healSpeedTime;
    public int HPHealValue => hpHeal;
    public int MPHealValue => mpHeal;

    public int InCreaseDamageValue => increaseDamage;
    public float InCreaseSpeedValue => increaseSpeed;
    public float DurationTime => durationTime;

    public void GetItemCount() => itemCnt++;
    public void GetItemCount(int i) => itemCnt += i;
    public void SetItemCount(int i) => itemCnt = i;

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
