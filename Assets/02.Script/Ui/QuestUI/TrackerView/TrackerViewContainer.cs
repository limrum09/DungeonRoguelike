using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackerViewContainer : MonoBehaviour
{
    [SerializeField]
    private RectTransform thisRect;
    [SerializeField]
    private TextMeshProUGUI questTitle;
    [SerializeField]
    private TrackerViewTask taskPrefab;

    private Quest inputQuest;
    public Quest InputQuest => inputQuest;

    public RectTransform containerRect => thisRect;

    public void TrackerViewContainerStart()
    {
        SetTrackerViewContainerSize();

        if (inputQuest == null)
            RemoveTrackerView();
    }

    public void SetTrackerView(Quest quest)
    {
        if (quest == null)
        {
            TrackerViewContainerStart();
            return;
        }
            

        inputQuest = quest;

        var newQuestTitle = Instantiate(questTitle, thisRect);
        newQuestTitle.text = $" {inputQuest.DisplayName}";

        foreach(var taskGroup in inputQuest.TaskGroups)
        {
            foreach(var task in taskGroup.Tasks)
            {
                var newTrackerTask = Instantiate(taskPrefab, thisRect);
                newTrackerTask.UpdateTaskText(task);
            }
        }

        newQuestTitle.transform.SetAsFirstSibling();

        SetTrackerViewContainerSize();
    }

    public void RemoveTrackerView()
    {
        inputQuest = null;

        questTitle.text = null;

        int childCount = transform.childCount;
        
        for(int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        this.gameObject.SetActive(false);
    }

    private void SetTrackerViewContainerSize()
    {
        float rectHeight = 0.0f;

        if(this.transform.childCount > 0)
            rectHeight = 40.0f;

        for (int i = 0; i < this.gameObject.transform.childCount - 1; i++)
        {
            rectHeight += 30.0f;
        }

        thisRect.sizeDelta = new Vector2(thisRect.sizeDelta.x, rectHeight);
    }
}
