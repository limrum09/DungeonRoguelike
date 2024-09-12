using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectDamageChild : MonoBehaviour
{
    [SerializeField]
    private SkillEffectDamage parent;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision Child tatget name : " + other.name);
        if (other.CompareTag("Enemy"))
            parent.TakeDamageToEnemy(other);
    }
}
