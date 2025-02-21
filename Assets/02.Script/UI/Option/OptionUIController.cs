using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIController : MonoBehaviour
{
    private void OnEnable()
    {
        Manager.Instance.canInputKey = false;
    }
    private void OnDisable()
    {
        Manager.Instance.canInputKey = true;
    }
}
