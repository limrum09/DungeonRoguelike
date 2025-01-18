using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public event Action<string, int> OnStatusChanged;  // Action<status type, status>
    public event Action<int, int, int> OnExpChanged;     // Action<current exp, level>

    [SerializeField]
    private int level;
    [SerializeField]
    private int health;
    [SerializeField]
    private int str;
    [SerializeField]
    private int dex;
    [SerializeField]
    private int luk;
    [SerializeField]
    private int bonusState;
    [SerializeField]
    private int exp;
    [SerializeField]
    private int currentExp;

    public PlayerStatusSaveData GetPlayerSaveStatus()
    {
        return new PlayerStatusSaveData
        {
            level = this.level,
            health = this.health,
            str = this.str,
            dex = this.dex,
            luk = this.luk,
            bonusState = this.bonusState,
            exp = this.exp,
            currentExp = this.currentExp,
            currentHp = PlayerInteractionStatus.instance.CurrentHP
        };
    }

    public void SetPlayerSavestatus(PlayerStatusSaveData saveData)
    {
        if (saveData == null)
        {
            FirstStart();
            return;
        }
            

        this.level = saveData.level;
        this.health = saveData.health;
        this.str = saveData.str;
        this.dex = saveData.dex;
        this.luk = saveData.luk;
        this.bonusState = saveData.bonusState;

        this.exp = saveData.exp;
        this.currentExp = saveData.currentExp;

        PlayerInteractionStatus.instance.CurrentHP = saveData.currentHp;

        OnStatusChanged?.Invoke("health", health);
        OnStatusChanged?.Invoke("str", str);
        OnStatusChanged?.Invoke("dex", dex);
        OnStatusChanged?.Invoke("luk", luk);
        OnStatusChanged?.Invoke("bonus", bonusState);
        OnExpChanged?.Invoke(currentExp, exp, level);
    }

    public void FirstStart()
    {
        level = 1;
        health = 5;
        str = 5;
        dex = 5;
        luk = 5;
        bonusState = 0;

        exp = 200;
        currentExp = 0;

        OnStatusChanged?.Invoke("health", health);
        OnStatusChanged?.Invoke("str", str);
        OnStatusChanged?.Invoke("dex", dex);
        OnStatusChanged?.Invoke("luk", luk);
        OnStatusChanged?.Invoke("bonus", bonusState);
        OnExpChanged?.Invoke(currentExp, exp, level);
    }

    public void StatusUP(string status)
    {
        if (bonusState <= 0)
            return;

        switch (status)
        {
            case "health":
                health++;
                OnStatusChanged?.Invoke("health", health);
                break;
            case "str":
                str++;
                OnStatusChanged?.Invoke("str", str);
                break;
            case "dex":
                dex++;
                OnStatusChanged?.Invoke("dex", dex);
                break;
            case "luk":
                luk++;
                OnStatusChanged?.Invoke("luk", luk);
                break;
        }

        bonusState--;
        OnStatusChanged?.Invoke("bonus", bonusState);
    }

    public void GetExp(int getExp)
    {
        if (level <= 100)
        {
            currentExp += getExp;

            while (currentExp >= exp)
            {
                currentExp -= exp;
                exp = exp + (level * 50) + 150;
                level++;
                bonusState += 5;
                OnStatusChanged?.Invoke("bonus", bonusState);
            }

            OnExpChanged?.Invoke(currentExp, exp, level);
        }
    }

}
