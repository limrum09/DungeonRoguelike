using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemEffectController : MonoBehaviour
{
    private bool followPlayer = false;
    private Transform player = null;
    private Vector3 playerPos;
    private float height;
    public void FindPlayer()
    {
        height = 3.0f;
        followPlayer = true;
    }

    private void FixedUpdate()
    {
        if (followPlayer)
        {
            FollowPlayer();
        }
    }

    private void Update()
    {
        if (followPlayer)
        {
            SetEffectTargetHeight();
        }
    }

    private void SetEffectTargetHeight()
    {
        if (height <= 0.7f)
            return;

        height -= 3.0f * Time.deltaTime;

        if (height < 0.7f)
            height = 0.7f;
    }

    private void FollowPlayer()
    {
        player = GameObject.FindGameObjectWithTag("PlayerComponent").transform;
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y + height, player.transform.position.z);
        transform.position = Vector3.Slerp(transform.position, playerPos, 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerComponent"))
        {
            Destroy(this.gameObject);
        }
    }
}
