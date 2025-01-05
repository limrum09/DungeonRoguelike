using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIController : MonoBehaviour
{
    private void OnEnable()
    {
        Manager.Instance.canUseShortcutKey = false;
    }
    private void OnDisable()
    {
        Manager.Instance.canUseShortcutKey = true;
    }
}
