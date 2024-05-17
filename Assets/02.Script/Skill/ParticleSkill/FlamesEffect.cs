using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamesEffect : MonoBehaviour
{
    public int damage;
    private float time;
    private bool timer;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        timer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0.0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            other.GetComponent<PlayerStatus>().TakeDamage(damage);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            timer = true;
            if(time >= 0.1f)
            {
                other.GetComponent<PlayerStatus>().TakeDamage(damage);
                time = 0.0f;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            timer = false;
        }
    }
}
