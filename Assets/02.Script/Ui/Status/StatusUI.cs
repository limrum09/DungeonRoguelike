using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    private StatusUIManager statusUIManager;

    private void OnEnable()
    {
        statusUIManager.ViewAndHideStateButton();
    }
}
