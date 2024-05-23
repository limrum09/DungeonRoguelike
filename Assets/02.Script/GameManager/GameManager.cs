using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private StatusUIManager statusUI;
    [SerializeField]
    private PlayerInteractionTest interactionTest;

    public int level;
    public int health;
    public int str;
    public int dex;
    public int luk;
    public int bonusState;

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

        level = 1;
        health = 5;
        str = 5;
        dex = 5;
        luk = 5;
    }


    private void Start()
    {
        ChangePlayerStatus();
    }

    public void ChangeCurrentExp()
    {
        statusUI.SetStatusUIText();
    }

    public void LevelUP()
    {
        level++;
        PlayerStatus.instance.Level++;
        bonusState += 5;
        statusUI.SetStatusUIText();
        statusUI.ViewAndHideStateButton();
    }

    public void ChangePlayerStatus()
    {
        var player = PlayerStatus.instance;
        var item = ItemStatus.instance;

        player.MaxHP = (health * 20) + (str * 5) + item.ItemHP;
        player.PlayerDamage = (str * 4) + (dex * 1) + item.ItemDamage;
        player.Sheild = (health * 2) + (str * 1) + (dex * 1) + item.ItemSheid;
        player.CriticalDamage = (int)(((str * 4) + (dex * 1) + item.ItemDamage + item.ItemCriticalDamage) * (100 + (luk * 2))) / 100;
        player.CriticalPer = (float)(luk * 0.5) + (float)(dex * 0.2) + item.ItemCriticalPer;
        player.PlayerSpeed = (float)9.75 + (float)(dex * 0.02) + (float)(str * 0.03) + item.ItemSpeed;
        player.SkillCoolTime = (float)9.8 + (float)((str + dex + health + luk) * 0.01) + item.ItemCoolTime;
        statusUI.SetStatusUIText();
    }

    public void InteractionTest(PlayerInteractionTest test)
    {
        this.interactionTest = test;
    }

    public void PlayerItemChangeTest(SelectTest item)
    {
        var changeItem = item.TestClone();

        if(changeItem is ArmorSelect armorItem)
        {
            interactionTest.ArmorChange(armorItem);
        }
        else if(changeItem is WeaponSelect weaponItem)
        {
            interactionTest.WeaponChange(weaponItem);
        }
    }
}
