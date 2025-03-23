using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void PlayerMove();

    [SerializeField]
    private GameObject player;

    [Header("Script")]
    [SerializeField]
    private ItemStatus itemStatus;
    [SerializeField]
    private PlayerStatus playerSaveStatus;
    [SerializeField]
    private InvenData invenData;
    [SerializeField]
    private GameManagerCoroutine gameManagerCoroutine;

    private PlayerController playerController;

    private int level;
    private int health;
    private int str;
    private int dex;
    private int luk;
    private int bonusStatus;

    private int buffDamage;
    private float buffSpeed;

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
    public int BonusStatus => bonusStatus;
    public int Exp => exp;
    public int CurrentExp => currentExp;


    public event PlayerMove playerMoveInGame;
    public void GameManagerStart()
    {
        isStart = true;

        gameManagerCoroutine.ListInitialized();
        buffDamage = 0;
        buffSpeed = 0;

        GameObject newPlayer = PlayerRespawnInRespawnPoint();

        playerSaveStatus.OnStatusChanged += SetStatusChanged;
        playerSaveStatus.OnExpChanged += SetExpChanged;
    }

    private void OnDestroy()
    {
        playerSaveStatus.OnStatusChanged -= SetStatusChanged;
        playerSaveStatus.OnExpChanged -= SetExpChanged;
    }

    public void PlayerMoveTransform()
    {
        playerMoveInGame?.Invoke();
    }

    public void AfterUIStartInGameManager()
    {
        invenData.InvenDataStart();
    }

    public void PlayerResurrection()
    {
        PlayerInteractionStatus.instance.Resurrection();
        ChangePlayerStatus();
    }

    private GameObject PlayerRespawnInRespawnPoint()
    {
        GameObject playerObject = Instantiate(player);
        playerObject.name = "PlayerObject";

        playerController = playerObject.GetComponent<PlayerController>();
        playerController.PlayerControllerStart();

        itemStatus.InteractionTest(playerObject.GetComponent<PlayerInteractionTest>());

        return playerObject;
    }

    private void LevelUP()
    {
        PlayerInteractionStatus.instance.CurrentHP = PlayerInteractionStatus.instance.MaxHP;
        BackendRank.Instance.RankInsert();
        Manager.Instance.UIAndScene.LevelUPUI();
    }

    public void SetExpChanged(int getExp, int getMaxExp, int getLevel)
    {
        if(level < getLevel)
        {
            level = getLevel;
            LevelUP();
        }

        currentExp = getExp;
        exp = getMaxExp;

        Manager.Instance.UIAndScene.ChangeEXPBar();
    }

    public void SetStatusChanged(string status, int value)
    {
        switch (status)
        {
            case "str":
                str = value;
                break;
            case "dex":
                dex = value;
                break;
            case "luk":
                luk = value;
                break;
            case "health":
                health = value;
                break;
            case "bonus":
                bonusStatus = value;
                break;
        }

        ChangePlayerStatus();
    }

    public void ChangePlayerStatus()
    {
        var player = PlayerInteractionStatus.instance;

        player.MaxHP = (health * 20) + (str * 5) + itemStatus.ItemHP;
        player.PlayerDamage = (str * 4) + (dex * 1) + itemStatus.ItemDamage + buffDamage;
        player.Sheild = (health * 2) + (str * 1) + (dex * 1) + itemStatus.ItemSheid;
        player.CriticalDamage = (int)(((str * 4) + (dex * 1) + itemStatus.ItemDamage + itemStatus.ItemCriticalDamage) * (100 + (luk * 2))) / 100;
        player.CriticalPer = (float)(luk * 0.5) + (float)(dex * 0.2) + itemStatus.ItemCriticalPer;
        player.PlayerSpeed = (float)9.75 + (float)(dex * 0.02) + (float)(str * 0.03) + itemStatus.ItemSpeed + buffSpeed;
        player.SkillCoolTime = (float)9.8 + (float)((str + dex + health + luk) * 0.01) + itemStatus.ItemCoolTime;

        if (level == 1 && isStart)
        {
            isStart = false;
            player.CurrentHP = player.MaxHP;
        }

        if (player.CurrentHP >= player.MaxHP)
            player.CurrentHP = player.MaxHP;

        playerController.SetAnimatorAttackSpeedValue(player.PlayerSpeed);

        ChangeHPBar();
    }

    public void PlayerStatusUp(string status) => playerSaveStatus.StatusUP(status);
    public void PlayerGetExp(int exp) => playerSaveStatus.GetExp(exp);
    public void PlayerUseSkillPoint(int skPoint) => playerSaveStatus.SetSkillPoint(-skPoint);
    public void PlayerDie() => Manager.Instance.UIAndScene.PlayerDie();
    public void PlayerWeaponChange(WeaponItem item) => itemStatus.ChangeWeaponItem(item);
    public void PlayerArmorChange(ArmorItem item) => itemStatus.ChangeArmorItem(item);
    public void ChangeHPBar()
    {
        // HPBar와 전체적인 값을 변경시킨다.
        Manager.Instance.UIAndScene.ChangeHPBar();
    }

    public void GetBuffItem(InvenItem item)
    {
        gameManagerCoroutine.PlayerBuffPotion(item);
    }
    public void GetBuffDamage(int getDamage)
    {
        buffDamage += getDamage;
        ChangePlayerStatus();
    }

    public void GetBuffSpeed(float getSpeed)
    {
        buffSpeed += getSpeed;
        ChangePlayerStatus();
    }
}
