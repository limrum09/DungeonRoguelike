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

    public void Following()
    {
        if(childTransform.position != this.transform.position)
        {
            Debug.Log("Follow Child!");
            childTransform.position = transform.position + offset;
        }
    }
}
