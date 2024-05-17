using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InvenPostionItem", menuName = "Scriptable Object/PostionItem")]
public class PostionItem : InvenItem
{
    public int hpHeal;
    public int mpHeal;
    public int increaseDamage;
    public float increaseSpeed;

    public float durationTime;
}
