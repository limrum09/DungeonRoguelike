using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Inactive,
    Running,
    Complete,
    Cancel,
    WaitingForCompletion
}

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest_")]
public class Quest : ScriptableObject
{
    #region Events
    public delegate void TaskSuccessChangedHandler(Quest quest, Task task, int currentSuccess, int prevSuccess);
    public delegate void CompletedHandler(Quest quest);
    public delegate void CanceledHandler(Quest quest);
    public delegate void NewTaskGroupHandler(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup);
    #endregion

    [SerializeField]
    private string questCode;
    [SerializeField]
    private QuestCategory category;
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string codeName;
    [SerializeField]
    private string displayName;
    [SerializeField, TextArea]
    private string desctiption;

    [Header("TaskGroup")]
    [SerializeField]
    private TaskGroup[] taskGroups;

    [Header("Reward")]
    [SerializeField]
    private Reward[] rewards;

    [Header("Option")]
    [SerializeField]
    private bool autoComplete;
    [SerializeField]
    private bool isCancelable;
    [SerializeField]
    private bool isSavable;

    [Header("Condition")]
    [SerializeField]
    private QuestCondition[] acceptionCondition;
    [SerializeField]
    private QuestCondition[] cancelCondition;

    private int currentTaskGroupIndex;

    public string QuestCode => questCode;
    public QuestCategory Category => category;
    public Sprite Icon => icon;
    public string CodeName => codeName;
    public string DisplayName => displayName;
    public string Description => desctiption;
    public QuestState State { get; private set; }
    public TaskGroup CurrentTaskGroup => taskGroups[currentTaskGroupIndex];
    public IReadOnlyList<TaskGroup> TaskGroups => taskGroups;
    public IReadOnlyList<Reward> Rewards => rewards;
    public bool IsRegistered => State != QuestState.Inactive;
    public bool IsCompletable => State == QuestState.WaitingForCompletion;
    public bool IsComplte => State == QuestState.Complete;
    public virtual bool IsCancelable => State == QuestState.Cancel;
    public bool IsAcceptable => acceptionCondition.All(x => x.IsPass(this));
    public virtual bool IsSavable => isSavable;

    public event TaskSuccessChangedHandler onTaskSuccessChanged;
    public event CompletedHandler onCompleted;
    public event CanceledHandler onCanceled;
    public event NewTaskGroupHandler onNewTaskGroup;
    // QuestStart라고 보는 것도 괜찮다.
    public void QuestRegisterd()
    {
        foreach(var taskGroup in taskGroups)
        {
            taskGroup.TaskGroupSetUP(this);

            foreach(var task in taskGroup.Tasks)
            {
                task.onSuccessChanged += OnSuccesschange;
            }
        }

        State = QuestState.Running;
        CurrentTaskGroup.TaskGroupStart();
    }

    public void QuestReceiveReport(string category, object target, int successCount)
    {
        if (IsComplte)
            return;

        CurrentTaskGroup.TaskGroupRecieveReport(category, target, successCount);

        // ReceiveReport로 값을 준 후, 현제 테스크의 조건이 전부 완료 되었을 경우
        if (CurrentTaskGroup.IsTaskAllComplete)
        {
            // Index의 값은 0부터 시작, 길이는 1부터 시작이기에 Index와 길이의 값이 같으면 진행할 TaskGroup이 없음
            if (currentTaskGroupIndex + 1 == taskGroups.Length)
            {
                // 기본값은 자동 성공이 아님, NPC의 퀘스트를 완료하면 다시 NPC에게 돌아가 완료 후 보상을 받는 방식
                State = QuestState.WaitingForCompletion;

                // 자동 성공일 경우
                if (autoComplete)
                {
                    QuestComplete();
                }
            }
            // Index의 값이 증가해도 taskGroups의 개수보다 적다면, 아직 진행해야 할 조건이 있음
            else
            {
                // 현제 진행 중인 TaskGroup
                var prevTaskGroup = taskGroups[currentTaskGroupIndex++];
                // 현제 진행 중이던 TaskGroup종료
                prevTaskGroup.TaskGroupEnd();

                CurrentTaskGroup.TaskGroupStart();
                onNewTaskGroup?.Invoke(this, CurrentTaskGroup, prevTaskGroup);
            }
        }
        else
            State = QuestState.Running;
    }

    public void QuestComplete()
    {
        foreach (var taskGroup in taskGroups)
            taskGroup.TaskGroupComplete();

        State = QuestState.Complete;

        // 보상 제공
        foreach(var reward in rewards)
            reward.Give(this);

        onCompleted?.Invoke(this);

        // 현제 event 초기화
        onTaskSuccessChanged = null;
        onCompleted = null;
        onCanceled = null;
        onNewTaskGroup = null;
    }

    // Quest는 취소 가능하지만, Acheivement(업적)은 취소가 불가능하기에 virtual로 선언
    public virtual void Cancel()
    {
        Debug.Assert(IsCancelable, "This Quest can't be canceled");

        State = QuestState.Cancel;
        onCanceled?.Invoke(this);
    }

    public Quest Clone()
    {
        var newClone = Instantiate(this);
        newClone.taskGroups = taskGroups.Select(x => new TaskGroup(x)).ToArray();

        return newClone;
    }

    public bool ContainsTarget(object target) => taskGroups.Any(x => x.ContainsTarget(this));
    public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);

    private void OnSuccesschange(Task task, int currentSuccess, int successCount) 
    {
        onTaskSuccessChanged?.Invoke(this, task, currentSuccess, successCount);
        // UIAndSceneManager.instance.QuestviewChange(this);
    }

    public QuestSaveData QuestSave()
    {
        return new QuestSaveData
        {
            questCode = this.questCode,
            state = this.State,
            taskGroupIndex = currentTaskGroupIndex,
            taskSuccessCount = CurrentTaskGroup.Tasks.Select(x => x.CurrentSuccess).ToArray()
        };
    }

    public void LoadQuest(QuestSaveData saveData)
    {
        State = saveData.state;
        currentTaskGroupIndex = saveData.taskGroupIndex;

        // 이전에 처리한 퀘스트
        for(int i = 0;i < currentTaskGroupIndex; i++)
        {
            var taskGroup = taskGroups[i];
            taskGroup.TaskGroupStart();
            taskGroup.TaskGroupComplete();
        }

        // 다시 진행 될 퀘스트 중, successCount가져오기
        for(int i = 0; i < saveData.taskSuccessCount.Length; i++)
        {
            CurrentTaskGroup.TaskGroupStart();
            CurrentTaskGroup.Tasks[i].CurrentSuccess = saveData.taskSuccessCount[i];
        }
    }
}
