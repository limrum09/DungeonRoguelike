using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskAction : ScriptableObject
{
    public abstract int TaskRun(Task task, int currentSuccess, int successCount);
}
