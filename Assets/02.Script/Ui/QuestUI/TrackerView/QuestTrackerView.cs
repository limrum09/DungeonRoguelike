using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestTrackerView : MonoBehaviour
{
    [SerializeField]
    private RectTransform thisRect;
    [SerializeField]
    private List<Quest> viewQuest = new List<Quest>();
    [SerializeField]
    private List<TrackerViewContainer> viewContainer = new List<TrackerViewContainer>();

    private int questIndex;

    public void TrackerViewStart()
    {
        // viewQuest에서 null일 경우 삭제        
        List<Quest> sortQuest = viewQuest.Where(x => x != null).ToList();

        viewQuest = sortQuest;

        if(viewQuest.Count <= 5)
        {
            int addIndex = 5 - viewQuest.Count;

            for(int i = 0; i < addIndex; i++)
            {
                viewQuest.Add(null);
            }
        }

        int questIndex = 0;
        for (int i = 0; i < 5; i++)
        {
            if (viewContainer[i].gameObject.activeSelf)
            {
                Quest getQuest = null;
                if (viewQuest[questIndex] != null)
                    getQuest = viewQuest[questIndex++];

                 viewContainer[i].SetTrackerView(getQuest);
            }                
        }

        SetTrackerViewSize();
    }
    
    public void QuestInputToTrackerView(Quest quest)
    {
        questIndex = -1;
        for(int i =0; i < 5; i++)
        {
            if (!this.transform.GetChild(i).gameObject.activeSelf)
            {
                questIndex = i;
                break;
            }       
        }

        if(questIndex <= -1)
        {
            return;
        }

        viewContainer[questIndex].gameObject.SetActive(true);
        viewContainer[questIndex].SetTrackerView(quest);

        viewQuest[questIndex] = quest;

        SetTrackerViewSize();
    }

    public void QuestRemoveToTrackerView(Quest quest)
    {
        for(int i = 0; i < 5; i++)
        {
            if(viewContainer[i].InputQuest == quest)
            {
                viewContainer[i].RemoveTrackerView();
                viewQuest[i] = null;

                break;
            }
        }

        SetTrackerViewSize();
    }

    private void SetTrackerViewSize()
    {
        float viewHeight = 10.0f;
        for(int i = 0; i < 5; i++)
        {
            if (viewContainer[i].gameObject.activeSelf)
                viewHeight += viewContainer[i].containerRect.sizeDelta.y;
        }

        thisRect.sizeDelta = new Vector2(thisRect.sizeDelta.x, viewHeight);
    }

    
}
