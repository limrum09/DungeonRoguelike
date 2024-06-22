using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackerViewTask : MonoBehaviour
{
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color completeColor;
    [SerializeField]
    private Color successCountColor;
    [SerializeField]
    private TextMeshProUGUI taskText;

    private Task currentTask;
    private void OnDestroy()
    {
        currentTask.onSuccessChanged -= UpdateTaskText;
    }

    public void TrackerViewSetup(Task task)
    {
        currentTask = task;
        currentTask.onSuccessChanged += UpdateTaskText;
        UpdateTaskText(task, 0, 0);
    }

    public void UpdateTaskText(Task task, int currnetSuccess, int prevSuccess)
    {
        if (task.IsComplete)
            taskText.text = BuildText(task, ColorCode(completeColor), ColorCode(completeColor));
        else
            taskText.text = BuildText(task, ColorCode(normalColor), ColorCode(successCountColor));
    }

    private string ColorCode(Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }

    private string BuildText(Task task, string textColorCode, string successCountColorCode)
    {
        return $"<color=#{textColorCode}> {task.Description} <color=#{successCountColorCode}> {task.CurrentSuccess} </color> / {task.NeedSuccessValue}</color>";
    }
}
