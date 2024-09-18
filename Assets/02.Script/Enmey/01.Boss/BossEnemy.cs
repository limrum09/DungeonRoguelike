using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
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

    private void ActiveSkill()
    {
        
    }

    public void ChangeBossPhase(int phase)
    {
        currnetPhase = phase;
    }

    public void TempActiveSkillButton(string skillCode)
    {
        foreach(var skill in phase2Skills)
        {
            if(skill.SkillCode == skillCode)
            {
                ActiveAnimation(skill.AnimationName);
                Debug.Log("스킬 : " + skill.AnimationName + " 실행");
                break;
            }
        }
    }

    private void ActiveAnimation(string skillName)
    {
        animator.Play(skillName);
    }
}
