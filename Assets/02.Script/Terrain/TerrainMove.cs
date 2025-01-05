using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMove : MonoBehaviour
{
    [SerializeField]
    private int index;

    public int TerrainIndex => index;
    public void SetIndex(int i)
    {
        index = i;
    }
    public void TerrainMoveToPosition(Vector3 vector)
    {
        this.transform.position = vector;
    }
}
