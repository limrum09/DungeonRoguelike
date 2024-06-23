using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Story/Story", fileName = "Story")]
public class QuestStory : ScriptableObject
{
    public bool isNPCTalk;

    [TextArea]
    public string[] storys;
}
