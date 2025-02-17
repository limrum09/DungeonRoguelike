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
    [SerializeField]
    private int price;

    [Header("HP & MP")]
    [SerializeField]
    private bool isSustain;         // 즉발성, 체력을 한번만 회복(false), 체력 여러번 회복(true)
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
    public int ItemPrice => price;

    public bool IsSustain => isSustain;
    public float HealSpeedTime => healSpeedTime;
    public int HPHealValue => hpHeal;
    public int MPHealValue => mpHeal;

    public int InCreaseDamageValue => increaseDamage;
    public float InCreaseSpeedValue => increaseSpeed;
    public float DurationTime => durationTime;

    public void AddItemCount() => itemCnt++;
    public void AddItemCount(int i)
    {
        itemCnt += i;

        if(ItemCnt > amount)
        {
            int overCnt = itemCnt - amount;
            itemCnt = amount;
            InvenData.instance.AddItem(Resources.Load<InvenItemDatabase>("InvenItemDatabase").FindItemBy(ItemCode), overCnt);
        }
    }
    public void SetItemCount(int i) => itemCnt = i;

    public void UsingItem()
    {
        Debug.Log("아이템 사용");
        if (increaseDamage >= 0.0f || increaseSpeed > 0.0f || isSustain)
            Manager.Instance.Game.GetBuffItem(this);
        else if (!isSustain)
        {
            PlayerInteractionStatus.instance.HealCurrentHP(hpHeal);
            return;
        }
    }

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
