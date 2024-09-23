using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListCheckTrackerViewButton : MonoBehaviour
{
    [SerializeField]
    private Sprite checkImage;
    [SerializeField]
    private Image checkbuttonImage;

    private bool trackViewCheck;
    private Quest currentQuest;

    public void SetQuest(Quest quest)
    {
        currentQuest = quest;
    }

    public void TrackerViewCheck()
    {
        if (trackViewCheck)
        {
            trackViewCheck = false;
            checkbuttonImage.sprite = null;

            Manager.Instance.UIAndScene.SetTackerViewQuest(currentQuest, trackViewCheck);
        }
        else
        {
            trackViewCheck = true;
            checkbuttonImage.sprite = checkImage;

            Manager.Instance.UIAndScene.SetTackerViewQuest(currentQuest, trackViewCheck);
        }
    }
}
