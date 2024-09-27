using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("Child")]
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private BossEffect bossEffectPosController;
    [SerializeField]
    private List<ActiveSkill> phase2Skills;
    [SerializeField]
    private List<ActiveSkill> phase3Skills;

    private RaycastHit playerHit;
    private int currnetPhase;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currnetPhase = 2;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
        animator.SetBool("Walk", true);

        Debug.DrawRay(this.transform.position, Vector3.forward * 100f, Color.red);
        if(Physics.Raycast(this.transform.position, Vector3.forward, out playerHit, 100f, playerLayer))
        {
            Debug.Log("레이 케스트에 플레어이 감지됨");
        }
        else
        {
            Debug.Log("감지 않됨");
        }
    }

    protected override void Attackanimation()
    {
        base.Attackanimation();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        nmAgent.speed = 7.0f;
    }

    public void ChangeBossPhase(int phase)
    {
        currnetPhase = phase;
    }

    public void TempActiveSkillButton(string skillCode)
    {
        List<ActiveSkill> selectList = null;

        if (currnetPhase == 2)
            selectList = phase2Skills;
        else if (currnetPhase == 3)
            selectList = phase3Skills;

        foreach(ActiveSkill skill in selectList)
        {
            if(skill.SkillCode == skillCode)
            {
                PlaySelectSkillAnimation(skill.AnimationName);
                bossEffectPosController.PlayBossEffect(skill);
                Debug.Log("스킬 : " + skill.AnimationName + " 실행");
                break;
            }
        }
    }

    private void PlaySelectSkillAnimation(string skillName)
    {
        animator.Play(skillName);
    }

    private void RandomSelectActiveSkill()
    {

    }

    private void BossRun()
    {
        nmAgent.speed = 10.0f;
        animator.SetBool("Run", true);
    }
}
