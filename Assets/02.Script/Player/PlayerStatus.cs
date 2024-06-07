using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    [SerializeField]
    private int level;
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
    [SerializeField]
    private int exp;
    [SerializeField]
    private int currentExp;

    public bool isDie;


    public int Level { get { return level; } set { level = value; } }
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int CurrentHP { get { return currentHP; } set { currentHP = value; } }
    public int PlayerDamage { get { return playerDamage; } set { playerDamage = value; } }
    public int CriticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }
    public int Sheild { get { return shied; } set { shied = value; } }
    public float CriticalPer { get { return criticalPer; } set { criticalPer = value; } }
    public float PlayerSpeed { get { return playerSpeed; } set { playerSpeed = value; } }
    public float SkillCoolTime { get { return skillCoolTime; } set { skillCoolTime = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public int CurrentExp { get { return currentExp; } set { currentExp = value; } }

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

        level = 1;
        maxHP = 0;
        currentHP = maxHP;
        playerDamage = 0;
        criticalDamage = 0;
        criticalPer = 0f;
        playerSpeed = 0f;
        skillCoolTime = 0f;
        isDie = false;

        if(level == 1)
        {
            exp = 200;
        }
    }

    public void InitializePlayerStatus()
    {
        level = 1;
        currentExp = 0;
        exp = 200;
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

    public void GetExp(int getExp)
    {
        if(level <= 100)
        {
            currentExp += getExp;

            while (currentExp >= exp)
            {
                currentExp -= exp;

                exp = exp + (level * 50) + 150;

                GameManager.instance.LevelUP();
            }

            GameManager.instance.ChangeExpBar();
        }
    }

    public void Resurrection()
    {
        isDie = false;
        this.gameObject.GetComponent<PlayerController>().animator.SetBool("Die", false);
    }

    public void Resurrection(int hp)
    {
        currentHP += hp;
        isDie = false;
        this.gameObject.GetComponent<PlayerController>().animator.SetBool("Die", false);
    }
}
