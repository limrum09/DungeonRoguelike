using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int currentHP;
    [SerializeField]
    protected int attackDamage;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected int exp;

    protected Animator animator;

    public int MaxHP
    {
        get { return maxHP; }
    }
    public int CurrentHP
    {
        get { return currentHP; }
    }
    public int AttackDamage
    {
        get { return attackDamage; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }
    public float WalkSpeed
    {
        get { return walkSpeed; }
    }

    public int Exp
    {
        get { return exp; }
    }

    // Start is called before the first frame update
    protected void Awake()
    {
        SetStatus(maxHP, attackDamage, attackSpeed, walkSpeed);
        currentHP = MaxHP;
    }

    protected void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public void TakeDamage(int damage)
    {
        if (!animator.GetBool("Die"))
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                currentHP = 0;
                animator.SetBool("Hit", false);
                GameManager.instance.PlayerGetExp(exp);
                EnemyDead();
            }
            else
            {
                EnemyHit();
            }            
        }
    }

    protected void SetStatus(int _hp, int _damage, float _attackSpeed, float _walkSpeed)
    {
        int hp = _hp;
        int damage = _damage;
        float attackSpeed = _attackSpeed;
        float walkSpeed = _walkSpeed;

        maxHP = SetStatusIntRange(hp);
        attackDamage = SetStatusIntRange(damage);
        this.attackSpeed =  SetStatusFloatRange(attackSpeed);
        this.walkSpeed = SetStatusFloatRange(walkSpeed);

        int sum = ((maxHP + attackDamage) * (Mathf.FloorToInt(attackSpeed) + 1) * (Mathf.FloorToInt(walkSpeed) + 1));

        exp = SetStatusIntRange(sum / 20);
    }

    protected int SetStatusIntRange(int _value)
    {
        int value = _value;

        int per = (value * 10) / 100;
        if (10 <= value && value < 50)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (50 <= value && value < 100)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (100 <= value && value < 300)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (300 <= value && value < 1000)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (1000 <= value)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }

        return value;
    }

    protected float SetStatusFloatRange(float _value)
    {
        float value = _value;

        float per = value / 10;
        if (1 <= value && value < 5)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (5 <= value && value < 10)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (10 <= value && value < 20)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }
        else if (20 <= value)
        {
            value = Random.Range(value - (per * 3), value + (per * 5));
        }

        return Mathf.Floor(value * 100f) / 100f;
    }

    protected void EnemyDead()
    {
        animator.SetBool("Die", true);
    }

    protected void EnemyHit()
    {
        animator.SetBool("Hit", true);
        GetComponent<Enemy>().EnemyHitMove();
    }
}
