using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementTaskView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI taskText;

    private Task currentTask;

    public void SetAchievementTaskView(Task _task)
    {
        currentTask = _task;

        currentTask.onSuccessChanged += UpdateTasktext;
        UpdateTasktext(currentTask, 0, 0);
    }

    public void UpdateTasktext(Task task, int currentSeccess, int prevSeuccess)
    {
        if (task.State == TaskState.Complete)
            taskText.text = BuildText(task, "#00B700");
        else
            taskText.text = BuildText(task, "#FF0000");
    }

    private string BuildText(Task task, string textColor)
    {
        return $"{task.Description} <color={textColor}>{task.CurrentSuccess} / {task.NeedSuccessValue}</color>";
    }

    private void OnDestroy()
    {
        currentTask.onSuccessChanged -= UpdateTasktext;
    }
}
