using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField]
    private BossStatus status;
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
    private float skillMoveTimeToTarget;
    private float skillMoveSpeed;

    private bool selectActionSkill = false;
    private bool BossMoving;
    private bool movingSkill;
    private bool arrivedAtTarget;

    private bool moveToTarget;
    private bool isWalk;

    private Vector3 skillAttackTarget;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currnetPhase = 2;
        BossMoving = true;
        moveToTarget = false;
        isWalk = true;

        nmAgent.speed = 1.0f;
        StartCoroutine(RandomActiveSkill());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // 스킬 사용 중 위치를 이동하는 경우 실행
        if(movingSkill && !arrivedAtTarget && moveToTarget)
        {
            nmAgent.speed = skillMoveSpeed; // 필요한 속도 설정

            transform.position = Vector3.MoveTowards(transform.position, skillAttackTarget, skillMoveSpeed * Time.deltaTime * 3);

            CheckArrivedToTarget();
        }
        else
        {
            if(checkMosterArea && !checkPlayer)
            {
                if (target != player)
                    target = player;
            }
        }
    }

    protected override void EnemyMove()
    {
        // 보스가 스킬을 사용할 경우 움직일 수 없음
        if (!BossMoving)
            return;
        base.EnemyMove();

        // 플레어이와의 거리
        float distanceToPlayer = Vector3.Distance(this.transform.position, target.transform.position);

        // 보스가 플레이어와 일정거리 떨어져 있을 경우 달린다
        if (distanceToPlayer >= 30f)
        {
            if(isWalk)
                Invoke("BossRun", 3f);
        }
        else if(distanceToPlayer <= 20f)
        {
            if (!isWalk)
            {
                BossWalk();
                CancelInvoke("BossRun");
            }
        }
    }

    protected override void Attackanimation()
    {
        base.Attackanimation();
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        nmAgent.speed = 1.0f;
    }

    // 페이즈 교체
    public void ChangeBossPhase(int phase)
    {
        currnetPhase = phase;
    }

    // 임시, 삭제 필요
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
                break;
            }
        }
    }

    private void PlaySelectSkillAnimation(string skillName)
    {
        animator.Play(skillName);
    }

    #region Aniamtion Event
    public void StartPlayingSkill()
    {
        Debug.Log("보스 스킬 시작");
        nmAgent.ResetPath();
        BossMoving = false;
    }
    public void EndPlayingSkill()
    {
        Debug.Log("보스 스킬 종료");
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
        nmAgent.speed = 7.0f;
    }
    #endregion

    private void BossRun()
    {
        isWalk = false;
        nmAgent.speed = 14.0f;
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
    }

    private void BossWalk()
    {
        isWalk = true;
        nmAgent.speed = 1.0f;
        animator.SetBool("Walk", true);
        animator.SetBool("Run", false);
    }

    // 스킬 사용 시, 타켓에 도착했는지 확인
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

    // 타겟에게 도착 했을 경우
    private void OnArrival()
    {
        movingSkill = false;
        // 경로 리셋
        nmAgent.ResetPath();
    }

    // 랜덤 스킬 사용 코루틴
    private IEnumerator RandomActiveSkill()
    {
        while (true)
        {
            if (useSkill)
            {
                selectActionSkill = false;
                List<ActiveSkill> selectList = null;

                // 현제 페이즈에 따른 스킬 선택
                if (currnetPhase == 1)
                    selectList = phase1Skills;
                else if (currnetPhase == 2)
                    selectList = phase2Skills;
                else if (currnetPhase == 3)
                    selectList = phase3Skills;

                // 스킬 발동 확율
                if (Random.Range(0, 9) <= 7)
                {
                    ActiveSkill randomSkill = null;
                    
                    // 스킬의 개수가 0이 아니면 스킬을 선택
                    if (selectList.Count == 0)
                        break;
                    else if (selectList.Count == 1)
                        randomSkill = selectList[0];
                    else if (selectList.Count >= 2)
                        randomSkill = selectList[Random.Range(0, selectList.Count)];

                    
                    if(randomSkill != null)
                    {
                        Vector3 normalizedDirectionToTarget = (target.position - this.transform.position).normalized;
                        skillAttackTarget = target.position - (normalizedDirectionToTarget * (attackAreaRadius / 3));

                        float distanceToTarget = Vector3.Distance(transform.position, skillAttackTarget);

                        // 스킬 움직임 범위보다 캐릭터가 멀리있다면, 스킬을 사용하지 않음
                        if (distanceToTarget > randomSkill.SkillMoveRange)
                        {
                            randomSkill = null;
                        }
                        // 범위안에 있어, 스킬 사용
                        else
                        {
                            // 에니메이션
                            PlaySelectSkillAnimation(randomSkill.AnimationName);

                            //이펙트
                            bossEffectPosController.PlayBossEffect(randomSkill);

                            // 스킬 사용 여부 확인
                            selectActionSkill = true;

                            // 스킬 사용 중 움직이 수 있는지 확인
                            movingSkill = randomSkill.CanMove;
                            skillMoveTimeToTarget = randomSkill.SkillMoveTime;
                        }
                    }
                }

                // 스킬이 선택되고, 움직이는 스킬일 경우
                if (selectActionSkill && movingSkill)
                {
                    // 타켓에 도착 했는지 유무 : false
                    arrivedAtTarget = false;

                    // 타켓 위치 정의
                    Vector3 normalizedDirectionToTarget = (target.position - this.transform.position).normalized;
                    skillAttackTarget = target.position - (normalizedDirectionToTarget * (attackAreaRadius / 3));

                    // '타켓과거 거리 / 소모하는 시간'으로 속도를 정함
                    float distanceToTarget = Vector3.Distance(transform.position, skillAttackTarget);
                    skillMoveSpeed = distanceToTarget / skillMoveTimeToTarget;
                }
            }
            // 일정 시간마다 반복
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}
