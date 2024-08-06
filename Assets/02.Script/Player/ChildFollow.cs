using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFollow : MonoBehaviour
{
    [SerializeField]
    private Transform childTransform;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - childTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = childTransform.position + offset;
    }
}
