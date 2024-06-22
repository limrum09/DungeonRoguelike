using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public delegate void QuestRegisteredHandler(Quest quest);
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    public delegate void QuestRecieveReportHnadler(Quest quest);


    public static QuestSystem instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<Quest> activeQuests = new List<Quest>();
    [SerializeField]
    private List<Quest> completedQuests = new List<Quest>();

    [SerializeField]
    private List<Quest> activeAchievement = new List<Quest>();
    [SerializeField]
    private List<Quest> completedAchievements = new List<Quest>();

    public event QuestRegisteredHandler onQuestRegisterd;
    public event QuestCompletedHandler onQuestCompleted;
    public event QuestCanceledHandler onQuestCanceled;

    public event QuestRegisteredHandler onAchievementRegisterd;
    public event QuestCompletedHandler onAchievementCompleted;

    public event QuestRecieveReportHnadler onQuestRecieveReport;


    public IReadOnlyList<Quest> ActiveQeusts => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;

    public IReadOnlyList<Quest> ActiveAchievements => activeAchievement;
    public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;


    // 게임 시작 시, 호출 (로드는 로드대로, 처음이면 처음대로, 퀘스트 기버 상관없이 호출됨)
    public Quest QuestSystemRegister(Quest quest)
    {
        var newQuest = quest.Clone();

        if(newQuest is Quest)
        {
            newQuest.onCompleted += OnQuestCompleted;
            newQuest.onCanceled += OnQuestCancel;

            activeQuests.Add(newQuest);

            newQuest.QuestRegisterd();
            onQuestRegisterd?.Invoke(newQuest);
        }
        // 업적인 경우
        else
        {
            newQuest.onCanceled += OnAchievementCompleted;

            activeAchievement.Add(newQuest);

            newQuest.QuestRegisterd();
            onAchievementRegisterd?.Invoke(newQuest);
        }

        return newQuest;
    }

    public void QuestSystemRecieveReport(List<Quest> quests, string category, object target, int successCount)
    {
        foreach (var quest in quests)
        {
            quest.QuestReceiveReport(category, target, successCount);
            onQuestRecieveReport?.Invoke(quest);
        }
            
    }
    public void QuestSystemRecieveReport(string category, object target, int successCount)
    {
        QuestSystemRecieveReport(activeQuests, category, target, successCount);
        QuestSystemRecieveReport(activeAchievement, category, target, successCount);
    }
    public void QuestSystemRecieveReport(QuestCategory category, TaskTarget target, int successCount) => QuestSystemRecieveReport(category.Category, target.Value, successCount);

    // 자동 완료가 아닌 수동 완료 퀘스트(들) 전체 완료
    public void CompletedWaitingQuests()
    {
        foreach(var quest in activeQuests)
        {
            if (quest.IsCompletable)
                quest.QuestComplete();
        }
    }

    // 진행 중인 퀘스트에서 매개변수가 있는지 확인
    public bool ContainsActiveQuest(Quest quest) => activeQuests.Any(x => x.CodeName == quest.CodeName);
    // 완료한 퀘스트에서 매개변수가 있는지 확인
    public bool ContainsCompletedQuest(Quest quest) => completedQuests.Any(x => x.CodeName == quest.CodeName);
    // 진행 중인 업적에서 매개변수가 있는지 확인
    public bool ContainsActiveAchievement(Quest quest) => activeAchievement.Any(x => x.CodeName == quest.CodeName);
    // 완료한 업적에서 매개변수가 있는지 확인
    public bool ContainsCompletedAchievements(Quest quest) => completedAchievements.Any(x => x.CodeName == quest.CodeName);

    public void LoadActiveQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = QuestSystemRegister(quest);
        newQuest.LoadQuest(saveData);
    }

    public void LoadCompletedQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = quest.Clone();
        newQuest.LoadQuest(saveData);

        if (newQuest is Quest)
        {
            completedQuests.Add(newQuest);
        }
        else
        {
            completedAchievements.Add(newQuest);
        }
    }


    private void OnQuestCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        onQuestCompleted?.Invoke(quest);
    }
    private void OnQuestCancel(Quest quest)
    {
        activeQuests.Remove(quest);

        Destroy(quest, Time.deltaTime);

        onQuestCanceled?.Invoke(quest);
    }
    private void OnAchievementCompleted(Quest quest)
    {
        activeAchievement.Remove(quest);
        completedAchievements.Add(quest);

        onAchievementCompleted?.Invoke(quest);
    }
}
