using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    [SerializeField]
    private float playerItemRotationX;
    [SerializeField]
    private float playerItemRotationY;
    [SerializeField]
    private float playerItemRotationZ;

    public float PlayerItemRotationX => playerItemRotationX;
    public float PlayerItemRotationY => playerItemRotationY;
    public float PlayerItemRotationZ => playerItemRotationZ;
}
