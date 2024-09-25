using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [Header("Script")]
    [SerializeField]
    private ItemStatus itemStatus;
    [SerializeField]
    private PlayerStatus playerSaveStatus;
    [SerializeField]
    private InvenData invenData;

    private PlayerController playerController;

    private int level;
    private int health;
    private int str;
    private int dex;
    private int luk;
    private int bonusState;

    private int exp;
    private int currentExp;

    private bool isStart;

    public PlayerController PlayerController => playerController;
    public PlayerStatus PlayerCurrentStatus => playerSaveStatus;
    public InvenData InvenDatas => invenData;

    public int Level => level;
    public int Health => health;
    public int Str => str;
    public int Dex => dex;
    public int Luk => luk;
    public int Exp => exp;
    public int CurrentExp => currentExp;
    public int BonusState => bonusState;

    public void GameManagerStart()
    {
        isStart = true;

        SceneManager.sceneLoaded += OnSceneLoad;

        GameObject newPlayer = PlayerRespawnInRespawnPoint();
            

        playerSaveStatus.FirstStart();
        
        ChangeExpBar();
    }

    public void AfterUIStartInGamemanager()
    {
        invenData.InvenDataStart();
    }

    private GameObject PlayerRespawnInRespawnPoint()
    {
        GameObject playerObject = Instantiate(player);
        playerObject.name = "PlayerObject";
        playerObject.transform.SetParent(null);

        playerController = playerObject.GetComponent<PlayerController>();
        playerController.PlayerControllerStart();

        itemStatus.InteractionTest(playerObject.GetComponent<PlayerInteractionTest>());

        return playerObject;
    }

    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        
    }

    public void InitializeGameManager()
    {
        isStart = true;

        ChangePlayerStatus();
        ChangeExpBar();
    }

    public void ChangeCurrentExp()
    {
        ChangeExpBar();
    }

    public void LevelUP()
    {
        PlayerInteractionStatus.instance.CurrentHP = PlayerInteractionStatus.instance.MaxHP;

        ChangeExpBar();

        Manager.Instance.UIAndScene.LevelUPUI();
    }

    private void GetPlayerStatus()
    {
        level = playerSaveStatus.Level;
        health = playerSaveStatus.Health;
        str = playerSaveStatus.Str;
        dex = playerSaveStatus.Dex;
        luk = playerSaveStatus.Luk;
        bonusState = playerSaveStatus.BonusStatus;

        exp = playerSaveStatus.Exp;
        currentExp = playerSaveStatus.CurrentExp;
    }

    public void ChangePlayerStatus()
    {
        var player = PlayerInteractionStatus.instance;

        GetPlayerStatus();

        player.MaxHP = (health * 20) + (str * 5) + itemStatus.ItemHP;
        player.PlayerDamage = (str * 4) + (dex * 1) + itemStatus.ItemDamage;
        player.Sheild = (health * 2) + (str * 1) + (dex * 1) + itemStatus.ItemSheid;
        player.CriticalDamage = (int)(((str * 4) + (dex * 1) + itemStatus.ItemDamage + itemStatus.ItemCriticalDamage) * (100 + (luk * 2))) / 100;
        player.CriticalPer = (float)(luk * 0.5) + (float)(dex * 0.2) + itemStatus.ItemCriticalPer;
        player.PlayerSpeed = (float)9.75 + (float)(dex * 0.02) + (float)(str * 0.03) + itemStatus.ItemSpeed;
        player.SkillCoolTime = (float)9.8 + (float)((str + dex + health + luk) * 0.01) + itemStatus.ItemCoolTime;

        if (level == 1 && isStart)
        {
            isStart = false;
            player.CurrentHP = player.MaxHP;
        }

        if (player.CurrentHP >= player.MaxHP)
            player.CurrentHP = player.MaxHP;

        ChangeHPBar();
    }

    public void PlayerStatusUp(string status) => playerSaveStatus.StatusUP(status);
    public void PlayerGetExp(int exp) => playerSaveStatus.GetExp(exp);

    public void PlayerWeaponChange(WeaponItem item) => itemStatus.ChangeWeaponItem(item);
    public void PlayerArmorChange(ArmorItem item) => itemStatus.ChangeArmorItem(item);
    public void ChangeHPBar()
    {
        GetPlayerStatus();

        Manager.Instance.UIAndScene.ChangeHPBar();
    }
    public void ChangeExpBar()
    {
        GetPlayerStatus();

        Manager.Instance.UIAndScene.ChangeEXPBar();
    }
}
