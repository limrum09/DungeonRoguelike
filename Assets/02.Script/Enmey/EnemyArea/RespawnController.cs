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
    private BoxCollider boxCollider;

    private void Start()
    {
        if (respawnTime <= 20f)
            respawnTime = 20f;

        for (int i = 0; i < respaenPoints.Count; i++)
            if(respaenPoints[i] != null) 
                respaenPoints[i].RespawnPointStart();

        StartCoroutine(RespawnObejct());
    }

    private void OnApplicationQuit()
    {
        StopCoroutine(RespawnObejct());
    }

    // 일정 시간(respawnTime)마다 자동으로 CheckInArea실행
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

    // 일정 지역안에 몬스터의 개수를 새아려서 리스폰하기
    private void CheckInArea(RespawnPoint point)
    {
        if (boxCollider.bounds.Contains(point.RespawnArea.bounds.min) && boxCollider.bounds.Contains(point.RespawnArea.bounds.max))
            point.RespawnObject();
    }
}
