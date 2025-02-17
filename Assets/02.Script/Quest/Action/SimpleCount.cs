using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/SimpleCount", fileName = "SimpleCount")]
public class SimpleCount : TaskAction
{
    public override int TaskRun(Task task, int currentSuccess, int successCount)
    {
        return currentSuccess + successCount;
    }
}
