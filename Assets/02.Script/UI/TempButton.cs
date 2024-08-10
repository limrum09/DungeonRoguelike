using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButton : MonoBehaviour
{
    public void ShortCutReset()
    {
        Manager.Instance.Key.ResetKeyCode();
    }
}
