using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Status")]
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

    [Header("Drop Info")]
    [SerializeField]
    private ScriptableObjectItem[] dropitem;
    [SerializeField]
    private float dropPer;

    [Header("Audio")]
    [SerializeField]
    protected AudioClip[] audioClips;

    protected Animator animator;
    protected AudioSource audioSource;

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
    protected virtual void Awake()
    {
        SetStatus(maxHP, attackDamage, attackSpeed, walkSpeed);
        currentHP = MaxHP;
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
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
                Manager.Instance.Game.PlayerGetExp(exp);
                EnemyDead();
            }
            else
            {
                EnemyHit();
            }
            animator.Play("Hit");
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
        ItemDrop();
        Destroy(this.transform.parent.gameObject, 2f);
    }

    protected void EnemyHit()
    {
        animator.SetBool("Hit", true);
        animator.SetBool("Die", false);

        if(audioClips[0] != null && audioClips != null)
        {
            audioSource.PlayOneShot(audioClips[1]);
        }
    }

    protected void ItemDrop()
    {
        Debug.Log("아이템 드랍 확인");
        if(dropitem.Length > 0)
        {
            Debug.Log("생성 가능한 아이템 확인 완료");
            if (Random.Range(0.0f, 1.0f) <= dropPer)
            {
                Debug.Log("아이템 생성");
                int index = Random.Range(0, dropitem.Length - 1);
                Vector3 dropPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

                Instantiate(dropitem[index], dropPos, Quaternion.Euler(-90f, 0f, 0f));
            }
        }
    }
}
