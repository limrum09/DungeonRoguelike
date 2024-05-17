using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<EvilMageAttack>();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("BallMoveing", 1.0f);
    }

    private void BallMoveing()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.07f);

        Invoke("DestroyedBall", 5f);
    }

    private void DestroyedBall()
    {
        
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {   
            DestroyedBall();
        }
    }
}
