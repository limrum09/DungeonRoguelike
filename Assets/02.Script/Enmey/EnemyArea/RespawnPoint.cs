using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [Header("Respawn Info")]
    [SerializeField]
    private GameObject respawnObject;
    [SerializeField]
    private int minRespwanCount;
    [SerializeField]
    private int maxRespwanCount;
    [SerializeField]
    private float noRespawnAreaRadius;

    [Header("Respawn Area")]
    [SerializeField]
    private SphereCollider respawnArea;

    [SerializeField]
    private List<GameObject> respawnObjects = new List<GameObject>();
    public SphereCollider RespawnArea => respawnArea;
    private float radius => respawnArea.radius;

    public void RespawnPointStart()
    {
        for (int i = 0; i < maxRespwanCount; i++)
            respawnObjects.Add(null);
    }

    public void RespawnObject()
    {
        // 현제 리스폰 된 오브젝트의 개수
        int currentObejctCount = CheckObjectCount();

        // 리스폰된 오브젝트가 최소 값보다 적을 경우 실행
        if (currentObejctCount < minRespwanCount)
        {            
            int respawnCount = Random.Range(minRespwanCount, maxRespwanCount);
            for (int i = currentObejctCount; i < respawnCount; i++)
            {
                GameObject respawn = Instantiate(respawnObject, SetRandomPosition(), Quaternion.identity);
                respawn.transform.SetParent(this.transform);
                respawnObjects[i] = respawn;
            }
        }
    }

    // 리스폰할 좌표 받기
    private Vector3 SetRandomPosition()
    {
        // 랜덤한 위치값 받기
        // Random.insideUnitSphere => 현제 좌표를 기준으로 반지름 길이가 1인 원을 그린후, 랜덤한 좌표를 준다. 
        // 현제 위치 + (현제 위치 기준으로 반지름 길이의 원 안에서의 랜덤한 좌표)
        Vector3 pos = transform.position + (Random.insideUnitSphere * radius);
        pos.y = transform.position.y;
        float areaXZ = Vector3.Distance(pos, transform.position);

        Debug.Log("Respawn Position : " + (pos - transform.position) + ", Distance to center : " + areaXZ + ", Radius : " + noRespawnAreaRadius);

        if (areaXZ < noRespawnAreaRadius)
        {
            float posX = pos.x - transform.position.x;
            float posZ = pos.z - transform.position.z;
            Debug.Log("AreaXZ : " + areaXZ + ", Radius : " + noRespawnAreaRadius);
            float emptyLine = noRespawnAreaRadius - areaXZ;

            if (Mathf.Abs(posX) > Mathf.Abs(posZ))
            {
                if (posX >= 0)
                    pos.x += emptyLine;
                else
                    pos.x -= emptyLine;
            }
            else
            {
                if (posZ >= 0)
                    pos.z += emptyLine;
                else
                    pos.z -= emptyLine;
            }

            Debug.Log("After Respawn Position : " + (pos - transform.position) + ", Distance to center : " + Vector3.Distance(pos, transform.position));
            Debug.Log("Position : " + pos);
        }

        // 현제 terrain을 기준으로 월드 공간의 높이를 샘플링한다.
        pos.y = Terrain.activeTerrain.SampleHeight(pos);

        return pos;
    }

    public int CheckObjectCount()
    {
        int objectCount = 0;

        /*        Vector3 capsulePos1 = transform.position;
                capsulePos1.y = -100f;
                Vector3 capsulePos2 = transform.position;
                capsulePos2.y = +100f;

                // 반지름 크기의 캡슐을 그린 후, 캡슐 안에 있는 모든 collider를 받아온다.
                Collider[] colliders = Physics.OverlapCapsule(capsulePos1, capsulePos2, radius);

                // 받아온 collider 중 tag:Enemy의 개수를 세아린다.
                foreach(var collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        objectCount++;
                    }

                }*/

        for (int i = 0; i < maxRespwanCount; i++)
            if (respawnObjects[i] != null)
                objectCount++;

        return objectCount;
    }
}
