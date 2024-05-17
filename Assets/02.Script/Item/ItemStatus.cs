using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatus : MonoBehaviour
{
    public static ItemStatus instance;

    [SerializeReference]
    private int itemHp;
    [SerializeReference]
    private int itemDamage;
    [SerializeReference]
    private int itemCriticalDamage;
    [SerializeReference]
    private int itemSheild;
    [SerializeReference]
    private float itemCriticalPer;
    [SerializeReference]
    private float itemSpeed;
    [SerializeReference]
    private float itemCoolTime;

    public int ItemHP { get { return itemHp; } set { itemHp = value; } }
    public int ItemDamage { get { return itemDamage; } set { itemDamage = value; } }
    public int ItemCriticalDamage { get { return itemCriticalDamage; } set { itemCriticalDamage = value; } }
    public int ItemSheid { get { return itemSheild; } set { itemSheild = value; } }
    public float ItemCriticalPer { get { return itemCriticalPer; } set { itemCriticalPer = value; } }
    public float ItemSpeed { get { return itemSpeed; } set { itemSpeed = value; } }
    public float ItemCoolTime { get { return itemCoolTime; } set { itemCoolTime = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        itemHp = 0;
        itemDamage = 0;
        itemCriticalDamage = 0;
        itemSheild = 0;
        itemCriticalPer = 0f;
        itemSpeed = 0f;
        itemCoolTime = 0f;
    }

    public void PlayerAddItemStatus()
    {
        GameManager.instance.ChangePlayerStatus();
    }

    public void PlayerLostItemStatus()
    {
        GameManager.instance.ChangePlayerStatus();
    }
}
