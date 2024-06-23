using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDetailViewController : MonoBehaviour
{
    [Header("Title")]
    [SerializeField]
    private TextMeshProUGUI titleText;

    [Header("Description")]
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [Header("Task")]
    [SerializeField]
    private TaskContentSize taskSize;
    [SerializeField]
    private GameObject taskContent;
    [SerializeField]
    private TaskContainerController taskContainerPrefab;
    [SerializeField]
    private int taskContainerCount;

    [Header("Reward")]
    [SerializeField]
    private RewardContentSize rewardSize;
    [SerializeField]
    private GameObject rewardContent;
    [SerializeField]
    private RewardContainer rewardContainerPrefab;
    [SerializeField]
    private int rewardContainerCount;

    // Object Pooling을 위한 List만들기
    private List<TaskContainerController> taskContainer;
    private List<RewardContainer> rewardContainer;

    private Quest currentQuest;

    public void DetailViewStart()
    {
        taskContainer = CreatePool(taskContainerPrefab, taskContainerCount, taskContent);
        rewardContainer = CreatePool(rewardContainerPrefab, rewardContainerCount, rewardContent);


        UIAndSceneManager.instance.onSelectQuestListView += ChangeQuestView;
    }

    private void OnApplicationQuit()
    {
        UIAndSceneManager.instance.onSelectQuestListView -= ChangeQuestView;
    }

    // Object Pool 생성
    private List<T> CreatePool<T>(T prefab, int count, GameObject parent) where T : MonoBehaviour
    {
        var pool = new List<T>(count);

        for(int i = 0;i < count; i++)
        {
            pool.Add(Instantiate(prefab, parent.transform));
        }

        return pool;
    }

    public void QuestSuccessCountChange(Quest quest)
    {
        if (currentQuest != quest)
            return;


    }

    // QuestList에서 Quest를 선택하면 호출
    public void ChangeQuestView(Quest quest)
    {
        if (quest == null)
            return;

        for (int i = 0; i < taskContainer.Count; i++)
        {
            taskContainer[i].gameObject.SetActive(false);
        }

        currentQuest = quest;

        titleText.text = quest.DisplayName;
        descriptionText.text = quest.Description;

        // taskContainerCount만틈 Pool Object가 만들어 졌다. 만들어진 순서대로 Task를 채워 넣기위해 Index를 0으로 초기화
        // Quest가 바뀔 때 마다 호출됨
        int taskIndex = 0;
        foreach(var taskGroup in quest.TaskGroups)
        {
            foreach(var task in taskGroup.Tasks)
            {
                if(task != null)
                {
                    var poolObject = taskContainer[taskIndex++];
                    
                    // Text 업데이트
                    poolObject.DetailViewTaskSetup(task);

                    // 사용 할 Pool Object보이기
                    poolObject.gameObject.SetActive(true);
                }
            }
        }

        // 사용되지 않은 Pool Object는 안보이도록 만듬
        for(int i = taskIndex; i < taskContainer.Count; i++)
        {
            taskContainer[i].gameObject.SetActive(false);
        }

        // content크기 재설정
        taskSize.UpdateTaskContentSize();

        // 이하동문
        int rewardIndex = 0;
        foreach(var reward in quest.Rewards)
        {
            if(reward is InvenItemReward invenReward)
            {
                var poolObject = rewardContainer[rewardIndex++];
                poolObject.gameObject.SetActive(true);

                poolObject.AddItemReward(invenReward);
            }
        }

        for(int i = rewardIndex; i < rewardContainer.Count; i++)
        {
            rewardContainer[i].gameObject.SetActive(false);
        }

        rewardSize.UpdateRewardContentSize();
    }
}
