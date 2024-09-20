using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("Child")]
    [SerializeField]
    private BossEffect bossEffectPosController;
    [SerializeField]
    private List<ActiveSkill> phase2Skills;
    [SerializeField]
    private List<ActiveSkill> phase3Skills;

    private int currnetPhase;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        currnetPhase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void RandomSelectActiveSkill()
    {
        
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
}
