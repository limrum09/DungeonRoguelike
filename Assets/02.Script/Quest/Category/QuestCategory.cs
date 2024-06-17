using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Category/Category", fileName = "QuestCategory_")]
public class QuestCategory : ScriptableObject
{
    [SerializeField]
    private string category;

    public string Category => category;
}
