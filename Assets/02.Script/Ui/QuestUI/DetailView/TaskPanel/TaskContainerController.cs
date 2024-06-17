using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskContainerController : MonoBehaviour
{
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color taskCompleteColor;
    [SerializeField]
    private Color taskSuccessColor;
    [SerializeField]
    private TextMeshProUGUI taskText;

    public void UpdateTaskText(Task task)
    {
        if (task.IsComplete)
            taskText.text = BuildText(task, ColorCode(taskCompleteColor), ColorCode(taskCompleteColor));
        else
            taskText.text = BuildText(task, ColorCode(normalColor), ColorCode(taskSuccessColor));
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
