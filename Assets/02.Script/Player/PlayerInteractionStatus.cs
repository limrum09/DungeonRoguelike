using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionStatus : MonoBehaviour
{
    public static PlayerInteractionStatus instance;

    private PlayerController playerController;

    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private int playerDamage;
    [SerializeField]
    private int criticalDamage;
    [SerializeField]
    private int shied;
    [SerializeField]
    private float criticalPer;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float skillCoolTime;

    public bool isDie;


    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }
    public int PlayerDamage { get { return playerDamage; } set { playerDamage = value; } }
    public int CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }
    public int Sheild { get { return shied; } set { shied = value; } }
    public float CriticalPer { get { return criticalPer; } set { criticalPer = value; } }
    public float PlayerSpeed { get { return playerSpeed; } set { playerSpeed = value; } }
    public float SkillCoolTime { get { return skillCoolTime; } set { skillCoolTime = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        maxHP = 0;
        currentHP = maxHP;
        playerDamage = 0;
        criticalDamage = 0;
        criticalPer = 0f;
        playerSpeed = 0f;
        skillCoolTime = 0f;
        isDie = false;
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void InitializePlayerStatus()
    {
        isDie = false;
    }

    public void TakeDamage(int damage)
    {
        if (!isDie)
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                currentHP = 0;
                isDie = true;
            }

            GameManager.instance.ChangeHPBar();
        }
    }

    public void Resurrection()
    {
        isDie = false;
        playerController.Ani.SetBool("Die", false);
    }

    public void Resurrection(int hp)
    {
        currentHP += hp;
        isDie = false;
        playerController.Ani.SetBool("Die", false);
    }
}
