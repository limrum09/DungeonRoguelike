using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Story/Scenario", fileName = "Scenario")]
public class Scenario : ScriptableObject
{
    public bool isNPCTalk;

    [TextArea]
    public string[] storys;
}
