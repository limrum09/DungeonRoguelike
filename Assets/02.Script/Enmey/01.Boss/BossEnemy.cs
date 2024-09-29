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
    private List<ActiveSkill> phase1Skills;
    [SerializeField]
    private List<ActiveSkill> phase2Skills;
    [SerializeField]
    private List<ActiveSkill> phase3Skills;


    private int currnetPhase;
    private float walkSpeed;
    private float runSpeed;
    private float skillMoveTimeToTarget;
    private float skillMoveSpeed;

    private bool selectActionSkill = false;
    private bool BossMoving;
    private bool movingSkill;
    private bool arrivedAtTarget;

    private bool moveToTarget;

    private Vector3 skillAttackTarget;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currnetPhase = 2;
        BossMoving = true;
        moveToTarget = false;

        nmAgent.speed = 1.0f;
        StartCoroutine(RandomActiveSkill());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(movingSkill && !arrivedAtTarget && moveToTarget)
        {
            nmAgent.speed = skillMoveSpeed; // 필요한 속도 설정
            Debug.Log("보스 위치 : " + this.transform.position);
            Debug.Log("스킬 이동 속도 : " + skillMoveSpeed + ", 타겟 위치 : " + skillAttackTarget + ", Player 위치 : " + target.position);
            // nmAgent.SetDestination(skillAttackTarget);

            //Vector3.MoveTowards(transform.position, skillAttackTarget, skillMoveSpeed * Time.deltaTime);
            Vector3.Lerp(transform.position, skillAttackTarget, 0.1f);

            CheckArrivedToTarget();
        }
    }

    protected override void EnemyMove()
    {
        if (!BossMoving)
            return;
        base.EnemyMove();
        animator.SetBool("Walk", true);

        float distanceToPlayer = Vector3.Distance(this.transform.position, target.transform.position);

        if (distanceToPlayer >= 30f)
        {
            Invoke("BossRun", 3f);
        }
        else if(distanceToPlayer <= 10f)
        {
            BossWalk();
        }
    }

    protected override void Attackanimation()
    {
        base.Attackanimation();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        nmAgent.speed = 1.0f;
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

    public void StartPlayingSkill()
    {
        BossMoving = false;
    }
    public void EndPlayingSkill()
    {
        BossMoving = true;
    }

    public void StartMoveToTarget()
    {
        moveToTarget = true;
    }

    public void EndMovetoTarget()
    {
        moveToTarget = false;
        skillAttackTarget = this.transform.position;
        nmAgent.speed = 1.0f;
        Debug.Log("---------------------------");
    }

    private void BossRun()
    {
        nmAgent.speed = 1.0f;
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
    }

    private void BossWalk()
    {
        nmAgent.speed = 1.0f;
        animator.SetBool("Walk", true);
        animator.SetBool("Run", false);
    }

    private void CheckArrivedToTarget()
    {
        // 도착 판단 거리 임계값
        if (!arrivedAtTarget && Vector3.Distance(transform.position, skillAttackTarget) < 1.0f) 
        {
            arrivedAtTarget = true; // 도착했음을 표시
            Debug.Log("목표지점에 도착했습니다!");
            OnArrival(); // 도착 시 실행할 메서드
        }
    }

    private void OnArrival()
    {
        movingSkill = false;
        // 경로 리셋
        nmAgent.ResetPath();
    }

    private IEnumerator RandomActiveSkill()
    {
        while (true)
        {
            if (useSkill)
            {
                selectActionSkill = false;
                List<ActiveSkill> selectList = null;

                if (currnetPhase == 1)
                    selectList = phase1Skills;
                else if (currnetPhase == 2)
                    selectList = phase2Skills;
                else if (currnetPhase == 3)
                    selectList = phase3Skills;

                Debug.Log("스킬 사용 판단, 현제 페이즈 : " + currnetPhase);
                if (Random.Range(0, 9) <= 7)
                {
                    ActiveSkill randomSkill = null;

                    Debug.Log("현제 페이즈 스킬 개수 : " + selectList.Count);

                    if (selectList.Count == 0)
                        break;
                    else if (selectList.Count == 1)
                        randomSkill = selectList[0];
                    else if (selectList.Count >= 2)
                        randomSkill = selectList[Random.Range(0, selectList.Count - 1)];

                    Debug.Log("스킬 검색 완료 : " + randomSkill.name);

                    if (randomSkill != null)
                    {
                        Debug.Log("스킬 사용");
                        PlaySelectSkillAnimation(randomSkill.AnimationName);
                        bossEffectPosController.PlayBossEffect(randomSkill);

                        movingSkill = randomSkill.CanMove;
                        skillMoveTimeToTarget = randomSkill.SkillMoveTime;
                        selectActionSkill = true;   
                    }   
                }

                if (selectActionSkill && movingSkill)
                {
                    Debug.Log("스킬 사용하면 몇번 작동함? ");
                    arrivedAtTarget = false;
                    Vector3 normalizedDirectionToTarget = (target.position - this.transform.position).normalized;
                    skillAttackTarget = target.position - (normalizedDirectionToTarget * (attackAreaRadius / 3));

                    float distanceToTarget = Vector3.Distance(transform.position, skillAttackTarget);
                    skillMoveSpeed = distanceToTarget / skillMoveTimeToTarget;
                }
            }

            yield return new WaitForSecondsRealtime(5f);
        }
    }
}
