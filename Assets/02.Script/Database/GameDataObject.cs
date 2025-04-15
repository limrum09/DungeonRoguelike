using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataObject : ScriptableObject
{
    [SerializeField]
    protected string code;
    public string Code => code;
}
