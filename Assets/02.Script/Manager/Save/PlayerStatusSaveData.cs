using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatusSaveData
{
    public int level;
    public int health;
    public int str;
    public int dex;
    public int luk;
    public int bonusState;
    public int exp;
    public int currentExp;
    public int currentHp;
    public int skillPoint;

    public void Reset()
    {
        level = 1;
        health = 5;
        str = 5;
        dex = 5;
        luk = 5;
        bonusState = 0;
        exp = 200;
        currentExp = 0;
        currentHp = 500;
        skillPoint = 0;
    }
}