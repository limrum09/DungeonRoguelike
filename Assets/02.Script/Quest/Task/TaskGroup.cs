using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TaskGroupState
{
    Inactive,
    Running,
    Complete
}

[System.Serializable]
public class TaskGroup
{
    [SerializeField]
    private Task[] tasks;

    public IReadOnlyList<Task> Tasks => tasks;
    public Quest Owner { get; private set; }
    public TaskGroupState State { get; private set; }
    public bool IsTaskAllComplete => tasks.Any(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;

    // Quest를 만들 시 Quest를 Clone으로 복제하는데, TaskGroup도 복제를 위해 호풀된다.
    public TaskGroup(TaskGroup copyTarget)
    {
        tasks = copyTarget.Tasks.Select(x => Object.Instantiate(x)).ToArray();
    }

    public void TaskGroupSetUP(Quest quest)
    {
        Owner = quest;
        foreach (var task in tasks)
            task.TaskSetup(quest);
    }

    public void TaskGroupStart()
    {
        State = TaskGroupState.Running;

        foreach (var task in tasks)
            task.TaskStart();
    }

    // Qeust 종료 시, 호출. 종료하는 것이 클리어일 경우도 있지만, 포기하는 경우도 있기에 State를 바꾸지 않는다.
    public void TaskGroupEnd()
    {
        foreach (var task in tasks)
            task.TaskEnd();
    }

    public void TaskGroupComplete()
    {
        if (IsComplete)
            return;

        State = TaskGroupState.Complete;

        foreach (var task in tasks)
        {
            if (!task.IsComplete)
                task.TaskComplete();
        }
    }

    public void TaskGroupRecieveReport(string category, object target, int successCount)
    {
        foreach(var task in tasks)
        {
            if(task.IsTarget(category, target))
                task.TaskRecieveReport(successCount);
        }
    }

    public Task FindTaskByTarget(object target) => tasks.FirstOrDefault(x => x.ContainsTarget(target));
    public Task FindTaskByTarget(TaskTarget target) => FindTaskByTarget(target.Value);

    public bool ContainsTarget(object target) => tasks.Any(x => x.ContainsTarget(target));
    public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);
}
