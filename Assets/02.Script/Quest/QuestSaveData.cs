using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSaveData
{
    public string questCode;
    public QuestState state;
    public int taskGroupIndex;
    public int[] taskSuccessCount;
}
