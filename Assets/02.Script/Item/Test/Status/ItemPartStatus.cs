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
}
