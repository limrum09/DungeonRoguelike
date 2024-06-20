using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveStatus : MonoBehaviour
{
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

    public int Level => level;
    public int Health => health;
    public int Str => str;
    public int Dex => dex;
    public int Luk => luk;
    public int BonusStatus => bonusState;
    public int Exp => exp;
    public int CurrentExp => currentExp;

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
    }

    public void StatusUP(string status)
    {
        bool useState = false;

        if (bonusState >= 1)
        {
            switch (status)
            {
                case "health":
                    health++;
                    useState = true;
                    break;
                case "str":
                    str++;
                    useState = true;
                    break;
                case "dex":
                    dex++;
                    useState = true;
                    break;
                case "luk":
                    luk++;
                    useState = true;
                    break;
                default:
                    useState = false;
                    break;
            }

            if (useState)
            {
                bonusState--;
                GameManager.instance.ChangePlayerStatus();
            }
            else
                Debug.Log("Can't use bonus status");
        }
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
            }

            GameManager.instance.ChangeExpBar();
        }
    }

}
