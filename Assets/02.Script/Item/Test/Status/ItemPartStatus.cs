using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPartStatus : MonoBehaviour
{
    [SerializeField]
    private int itemHp;
    [SerializeField]
    private int itemDamage;
    [SerializeField]
    private int itemCriticalDamage;
    [SerializeField]
    private int itemSheild;
    [SerializeField]
    private float itemCriticalPer;
    [SerializeField]
    private float itemSpeed;
    [SerializeField]
    private float itemCoolTime;

    public int ItemHP => itemHp;
    public int ItemDamage => itemDamage;
    public int ItemCriticalDamage => itemCriticalDamage;
    public int ItemSheild => itemSheild;
    public float ItemCriticalPer => itemCriticalPer;
    public float ItemSpeed => itemSpeed;
    public float ItemCoolTime => itemCoolTime;

    public void StatusCopy(ItemPartStatus copyStatus)
    {
        //Debug.Log("Status Copy : " + copyStatus + ". hp : " + copyStatus.itemHp);
        if(copyStatus != null)
        {
            itemHp = copyStatus.ItemHP;
            itemDamage = copyStatus.ItemDamage;
            itemCriticalDamage = copyStatus.ItemCriticalDamage;
            itemSheild = copyStatus.ItemSheild;
            itemCriticalPer = copyStatus.ItemCriticalPer;
            itemSpeed = copyStatus.ItemSpeed;
            itemCoolTime = copyStatus.ItemCoolTime;
        }
        else
        {
            itemHp = 0;
            itemDamage = 0;
            itemCriticalDamage = 0;
            itemSheild = 0;
            itemCriticalPer = 0f;
            itemSpeed = 0f;
            itemCoolTime = 0f;
        }
        
    }
}
