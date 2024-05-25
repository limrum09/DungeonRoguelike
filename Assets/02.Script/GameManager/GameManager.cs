using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private StatusUIManager statusUI;
    [SerializeField]
    private ItemStatus itemStatus;

    public int level;
    public int health;
    public int str;
    public int dex;
    public int luk;
    public int bonusState;

    private bool isStart;

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

        FirstStart();

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        var uiManager = FindObjectOfType<StatusUIManager>();
        if (uiManager != null)
        {
            GetStatusUI(uiManager);
        }
    }

    private void FirstStart()
    {
        level = 1;
        health = 5;
        str = 5;
        dex = 5;
        luk = 5;

        isStart = true;
    }

    private void Start()
    {
        // ChangePlayerStatus();
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

        player.Level = level;
        player.MaxHP = (health * 20) + (str * 5) + itemStatus.ItemHP;
        player.PlayerDamage = (str * 4) + (dex * 1) + itemStatus.ItemDamage;
        player.Sheild = (health * 2) + (str * 1) + (dex * 1) + itemStatus.ItemSheid;
        player.CriticalDamage = (int)(((str * 4) + (dex * 1) + itemStatus.ItemDamage + itemStatus.ItemCriticalDamage) * (100 + (luk * 2))) / 100;
        player.CriticalPer = (float)(luk * 0.5) + (float)(dex * 0.2) + itemStatus.ItemCriticalPer;
        player.PlayerSpeed = (float)9.75 + (float)(dex * 0.02) + (float)(str * 0.03) + itemStatus.ItemSpeed;
        player.SkillCoolTime = (float)9.8 + (float)((str + dex + health + luk) * 0.01) + itemStatus.ItemCoolTime;

        if (player.Level == 1 && isStart)
        {
            isStart = false;
            player.CurrentHP = player.MaxHP;
        }

        statusUI.SetStatusUIText();
    }

    public void PlayerItemChange(SelectTest item)
    {
        var changeItem = item.TestClone();

        if(changeItem is ArmorSelect armorItem)
        {
            itemStatus.ChangeArmorItem(armorItem);
        }
        else if(changeItem is WeaponSelect weaponItem)
        {
            itemStatus.ChangeWaeponItem(weaponItem);
        }
    }

    public void ChangeHPBar() => UIAndSceneManager.instance.ChangeHPBar();

    public void ChangeExpBar() => UIAndSceneManager.instance.ChangeEXPBar();
    #region OnSceneLoad
    public void GetStatusUI(StatusUIManager UI)
    {
        this.statusUI = UI;
    }
    #endregion
}
