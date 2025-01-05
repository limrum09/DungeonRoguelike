using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTerrainController : MonoBehaviour
{
    [SerializeField]
    private TerrainMove[] terrains;

    [SerializeField]
    private Terrain[,] terrainArray = new Terrain[3, 3];

    private Vector3 terrainSize;
    private Vector3 playerPos;

    public TerrainMove[] Terrains => terrains;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 9; i++)
            terrains[i].SetIndex(i);
        // terrainSize = tempTerrain.terrainData.size;
        //terrainSize = new Vector3(100f, 0f, 100f);
        // SetTerrainArray();

    }
/*
    private void SetTerrainArray()
    {
        int cnt = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                terrainArray[i, j] = terrains[cnt];

                Debug.Log("지형 개수 : " + (cnt + 1) + "지형 값 : " + i + ", " + j);
                cnt++;
            }
        }
    }*/

    // Terrain의 Trigger에서 호출
    public void MoveTerrain(Terrain currentTerrain)
    {
        Vector3 terrainPos = currentTerrain.transform.position;

        // 모든 Terrain을 재배치
        for (int i = 0; i < terrains.Length; i++)
        {
            // 새로운 위치를 설정: 주변 8개의 Terrain을 재배치
            Vector3 newPosition = CalculateNewTerrainPosition(terrains[i].transform.position, terrainPos);
            terrains[i].TerrainMoveToPosition(newPosition);

            Debug.Log($"Terrain {i} moved to {newPosition}");
        }
    }

    public void MoveTerrain(int dir)
    {
        // 임시 배열 선언
        TerrainMove[] tempTerrains = new TerrainMove[9];
        // 배열 카피
        System.Array.Copy(terrains, tempTerrains, 9);

        switch (dir)
        {
            /*
            아래에서 위로 올라옴
            [0][1][2]
            [3][*4][5]
            [6][7][8]

            =>

            tempTerrains
            [0][*1][2]
            [3][4][5]
            [6][7][8]

            =>

            tempTerrains
            [3][*4][5]
            [6][7][8]
            [9][10][11]

            =>

            Terrains
            [0 => 9 - 9][1 => 10 - 9][2 => 11 -9]
            [3 => 3][4 => *4][5 => 5]
            [6 => 6][7 => 7][8 => 8]

            =>

            [6][7][8]
            [0][1][2]
            [3][4][5]
            */
            case 0:
                for(int i = 0; i < 9; i++)
                {
                    int index = i + 3;

                    if(index > 8)
                    {
                        terrains[index - 9] = tempTerrains[i];
                        Vector3 currentPos = tempTerrains[i].transform.position;
                        Vector3 terrainMovePos = new Vector3(currentPos.x + 600f, currentPos.y + 0f, currentPos.z + 0f);

                        Debug.Log("Index : " + dir);
                        Debug.Log("이동 오브젝트 순서 : " + i + ", 현제 위치 : " + currentPos + ", 이동 위치 : " + terrainMovePos);
                        terrains[index - 9].TerrainMoveToPosition(terrainMovePos);
                    }
                    else
                    {
                        terrains[index] = tempTerrains[i];
                    }
                }
                break;
            case 1:
                for(int i = 0; i < 9; i++)
                {
                    int index = i - 3;
                    if(index < 0)
                    {
                        terrains[index + 9] = tempTerrains[i];
                        Vector3 currentPos = tempTerrains[i].transform.position;
                        Vector3 terrainMovePos = new Vector3(currentPos.x - 600f, currentPos.y + 0f, currentPos.z + 0f);

                        Debug.Log("Index : " + dir);
                        Debug.Log("이동 오브젝트 순서 : " + i + ", 현제 위치 : " + currentPos + ", 이동 위치 : " + terrainMovePos);
                        terrains[index + 9].TerrainMoveToPosition(terrainMovePos);
                    }
                    else
                    {
                        terrains[index] = tempTerrains[i];
                    }
                }

                break;
            case 2:
                for(int i = 0; i < 9; i++)
                {
                    int index = i % 3;

                    if(index == 0)
                    {
                        terrains[i + 2] = tempTerrains[i];
                        Vector3 currentPos = tempTerrains[i].transform.position;
                        Vector3 terrainMovePos = new Vector3(currentPos.x + 0f, currentPos.y + 0f, currentPos.z - 600f);

                        Debug.Log("Index : " + dir);
                        Debug.Log("이동 오브젝트 순서 : " + i + ", 현제 위치 : " + currentPos + ", 이동 위치 : " + terrainMovePos);
                        terrains[i + 2].TerrainMoveToPosition(terrainMovePos);
                    }
                    else
                    {
                        terrains[i - 1] = tempTerrains[i];
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 9; i++)
                {
                    int index = i % 3;

                    if (index == 2)
                    {
                        terrains[i - 2] = tempTerrains[i];
                        Vector3 currentPos = tempTerrains[i].transform.position;
                        Vector3 terrainMovePos = new Vector3(currentPos.x + 0f, currentPos.y + 0f, currentPos.z + 600f);

                        Debug.Log("Index : " + dir);
                        Debug.Log("이동 오브젝트 순서 : " + i + ", 현제 위치 : " + currentPos + ", 이동 위치 : " + terrainMovePos);
                        terrains[i - 2].TerrainMoveToPosition(terrainMovePos);
                    }
                    else
                    {
                        terrains[i + 1] = tempTerrains[i];
                    }
                }
                break;
        }
    }

    // 새로운 Terrain 위치를 계산하는 함수
    private Vector3 CalculateNewTerrainPosition(Vector3 terrainCurrentPosition, Vector3 centerTerrainPosition)
    {
        // 기본적인 예로, 현재 위치에서 주변 Terrain의 새 위치를 설정
        float xOffset = terrainCurrentPosition.x - centerTerrainPosition.x;
        float zOffset = terrainCurrentPosition.z - centerTerrainPosition.z;

        Vector3 newPosition = new Vector3(centerTerrainPosition.x + xOffset, terrainCurrentPosition.y, centerTerrainPosition.z + zOffset);
        return newPosition;
    }
}
