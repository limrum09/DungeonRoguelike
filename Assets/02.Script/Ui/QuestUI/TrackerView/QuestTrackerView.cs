using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerView : MonoBehaviour
{
    [SerializeField]
    private RectTransform thisRect;
    [SerializeField]
    private List<TrackerViewContainer> viewContainer = new List<TrackerViewContainer>();

    private int questIndex;

    public void TrackerViewStart()
    {
        questIndex = 0;

        for(int i = 0; i < 5; i++)
        {
            if (viewContainer[i].gameObject.activeSelf)
            {
                viewContainer[i].TrackerViewContainerStart();
            }                
        }

        for (int i = 0; i < 5; i++)
        {
            if (viewContainer[i].gameObject.activeSelf)
            {
                questIndex++;
            }
        }

        SetTrackerViewSize();
    }
    
    public void QuestInputToTrackerView(Quest quest)
    {
        if (questIndex >= 5)
            return;

        questIndex++;
        viewContainer[questIndex].gameObject.SetActive(true);
        viewContainer[questIndex].SetTrackerView(quest);

        SetTrackerViewSize();
    }

    public void QuestRemoveToTrackerView(Quest quest)
    {
        int removeIndex = 0;

        for(int i = 0; i < 5; i++)
        {
            if(viewContainer[i].InputQuest == quest)
            {
                viewContainer[i].RemoveTrackerView();
                removeIndex = i;
                questIndex--;

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
