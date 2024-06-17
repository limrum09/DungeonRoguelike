using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TaskState
{
    Inactive,
    Running,
    Complete
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion

    [Header("Category")]
    [SerializeField]
    private QuestCategory category;     // Task의 Category

    [Header("Text")]
    [SerializeField]
    private string codeName;            // Task의 고유(Unique) 이름
    [SerializeField]
    private string description;         // Task 내용

    [Header("Action")]
    [SerializeField]
    private TaskAction action;          // Task가 값을 받는 방법, -1, 0, 1 등 어떤 값을 받을 경우 값을 어떻게 적용되는지 선택

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;       // Task의 target들 한개일 수 있지만, 여러개가 될 수 있다.

    [Header("Setting")]
    private InitialSuccessValue initialSuccessValue;
    [SerializeField]
    private int needSuccessValue;       // 완료하기 위해 필요한 값
    [SerializeField]
    private bool canReceiveReportsDuringCompletion;

    private TaskState state;
    private int currentSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
        }
    }

    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;

            currentSuccess = Mathf.Clamp(value, 0, needSuccessValue);

            if(currentSuccess!= prevSuccess)
            {
                State = currentSuccess == needSuccessValue ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }
        }
    }

    public QuestCategory QuestCategory => category;
    public string CodeNmae => codeName;
    public string Description => description;
    public int NeedSuccessValue => needSuccessValue;
    public bool IsComplete => State == TaskState.Complete;
    public Quest Owner { get; private set; }

    public void TaskSetup(Quest quest)
    {
        Owner = quest;
    }

    // 현제 Task가 시작
    public void TaskStart()
    {
        State = TaskState.Running;
        if (initialSuccessValue)
            CurrentSuccess = initialSuccessValue.GetValue(this);
    }

    // 현제 Task가 종료
    public void TaskEnd()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }

    // 현제 Task가 완료
    public void TaskComplete()
    {
        CurrentSuccess = needSuccessValue;
    }

    // 게임 실행 중 값이 변경 될 경우 기본적으로 호출
    public void TaskRecieveReport(int successCount)
    {
        CurrentSuccess = action.TaskRun(this, CurrentSuccess, successCount);
    }

    // 타겟이 맞는지 확인
    public bool IsTarget(string category, object target)
        => QuestCategory.Category == category &&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

    // Target이 Task의 targets에 있는지 확인
    public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}
