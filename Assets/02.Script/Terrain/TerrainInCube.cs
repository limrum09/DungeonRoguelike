using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInCube : MonoBehaviour
{
    private LobbyTerrainController lobbyTerrainController;

    // Start is called before the first frame update
    void Start()
    {
        lobbyTerrainController = this.transform.root.GetComponent<LobbyTerrainController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerPos = other.transform.position;
            Vector3 triggerPos = transform.position;
            Vector3 dirPos = playerPos - triggerPos;

            int dir = CheckDirection(playerPos, triggerPos);

            Debug.Log("충돌 지점 : " + other.transform.position + ", 큐브의 중심 : " + triggerPos + ", 충돌 지점과의 거리 좌표 : " + dirPos + ", 방향 : " + dir);

            lobbyTerrainController.MoveTerrain(dir);
        }
    }

    private int CheckDirection(Vector3 playerPos, Vector3 triggerPos)
    {
        Vector3 dir = playerPos - triggerPos;

        // y값(위, 아래)는 무시
        dir.y = 0;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            // Player is exiting from the left or right side
            if (dir.x > 0)
            {
                // Right side
                return 0;
            }
            else
            {
                // Left side
                return 1;
            }
        }
        else
        {
            // Player is exiting from the front or back side
            if (dir.z > 0)
            {
                // Front side
                return 3;
            }
            else
            {
                // Back side
                return 2;
            }
        }
    }
}
