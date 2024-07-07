using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Story/Scenario", fileName = "Scenario")]
public class Scenario : ScriptableObject
{
    public bool isNPCTalk;
    public bool isQuestSuggest; // 퀘스트 수락, 퀘스트에 하나만 있어도 충분함

    [TextArea]
    public string[] storys;
}
