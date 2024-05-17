using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBallInstantiate : MonoBehaviour
{
    public GameObject skillBall;
    public GameObject instantiatePos;

    private void Start()
    {
        
    }

    public void UsingSkill()
    {
        Instantiate(skillBall, instantiatePos.transform.position, instantiatePos.transform.rotation);
    }
}
