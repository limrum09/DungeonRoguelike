using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Collider groundCollider;

    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask GroundLayer;
    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<Collider>();
    }

    public bool IsGround()
    {
        return Physics.BoxCast(groundCollider.bounds.center, boxSize, -transform.up, transform.rotation, maxDistance, GroundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}
