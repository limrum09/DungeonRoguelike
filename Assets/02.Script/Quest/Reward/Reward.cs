using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : ScriptableObject
{
    [SerializeField]
    private Sprite icon;            // Icon
    [SerializeField]
    protected string description;     // 설명

    public Sprite Icon => icon;
    public string Description => description;

    public abstract void Give(Quest quest);
}
