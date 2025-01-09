using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TargetFollowing : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    private GameObject target;

    [Header("Option")]
    [SerializeField]
    private bool isDistance;
    
    public bool isFollowY;


    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;


    }
}
