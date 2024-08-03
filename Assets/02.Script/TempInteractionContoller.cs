using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInteractionContoller : MonoBehaviour
{
    public void TempGetExp(int exp)
    {
        Manager.Instance.Game.PlayerGetExp(exp);
    }
}
