using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    public GameObject dragonBreath;
    private ParticleSystem breathParticle;

    private void Start()
    {
        breathParticle = dragonBreath.GetComponent<ParticleSystem>();
    }
    public void BreathAttack()
    {
        dragonBreath.SetActive(true);

        breathParticle.Play();
    }
}
