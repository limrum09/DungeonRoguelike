using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField]
    private List<RespawnPoint> respaenPoints;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private BoxCollider collider;

    private void Start()
    {
        if (respawnTime <= 20f)
            respawnTime = 20f;

        StartCoroutine(RespawnObejct());
    }

    private void OnApplicationQuit()
    {
        StopCoroutine(RespawnObejct());
    }

    IEnumerator RespawnObejct()
    {
        while (true)
        {
            foreach (RespawnPoint respawn in respaenPoints)
            {
                if(respawn != null)
                    CheckInArea(respawn);
            }                

            yield return new WaitForSeconds(respawnTime);
        }
    }

    private void CheckInArea(RespawnPoint point)
    {
        if (collider.bounds.Contains(point.RespawnArea.bounds.min) && collider.bounds.Contains(point.RespawnArea.bounds.max))
            point.RespawnObject();
    }
}
