using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObject", fileName = "Target_")]
public class GameobjectTarget : TaskTarget
{
    [SerializeField]
    private GameObject value;
    public override object Value => value;

    public override bool IsEqual(object target)
    {
        var gameObjectTarget = target as GameObject;
        if (gameObjectTarget == null)
            return false;

        return gameObjectTarget.name.Contains(value.name);
    }
}
