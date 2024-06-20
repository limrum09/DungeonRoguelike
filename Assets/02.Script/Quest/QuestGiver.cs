using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    private void Start()
    {
        foreach(var quest in quests)
        {
            QuestSystem.instance.QuestSystemRegister(quest);
        }
    }
}
