using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemPositionInfo : MonoBehaviour
{
    [SerializeField]
    private float playerItemRotationX;
    [SerializeField]
    private float playerItemRotationY;
    [SerializeField]
    private float playerItemRotationZ;
    [SerializeField]
    private BoxCollider boxCollider;

    public float PlayerItemRotationX => playerItemRotationX;
    public float PlayerItemRotationY => playerItemRotationY;
    public float PlayerItemRotationZ => playerItemRotationZ;
    public BoxCollider WeaponCollider => boxCollider;
}
