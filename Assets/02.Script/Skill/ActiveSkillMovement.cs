using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillMovement : MonoBehaviour
{
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float aclSpeed;         // 가속도
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private string colliderTag;
    [SerializeField]
    private ParticleSystem newParticle;

    private float timer;
    private Vector3 direction;

    private void Awake()
    {
        timer = 0.0f;
    }

    private void Start()
    {
        direction = moveDirection.normalized;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delayTime)
        {
            this.transform.position += direction * Time.deltaTime * moveSpeed;
            moveSpeed += aclSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(colliderTag))
        {
            Instantiate(newParticle).transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
}
