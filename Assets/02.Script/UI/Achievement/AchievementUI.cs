using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AchievementViewState
{
    All,
    Running,
    Complete
}

public class AchievementUI : MonoBehaviour
{
    [SerializeField]
    private GameObject achievementViewUI;
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private AchievementDetailView achievementDetailView;
    [SerializeField]
    private List<AchievementDetailView> detailView = new List<AchievementDetailView>();
    [SerializeField]
    private List<AchievementToggle> toggles = new List<AchievementToggle>();

    private RectTransform contentRect;
    private float detailViewHeight;
    // Start is called before the first frame update
    public void AchievementUIStart()
    {
        var questSystem = Manager.Instance.Quest;

        for (int i = 0;i < questSystem.ActiveAchievements.Count; i++)
        {
            AddAchievement(questSystem.ActiveAchievements[i]);
        }

        for(int i  = 0; i < questSystem.CompletedAchievements.Count; i++)
        {
            AddAchievement(questSystem.CompletedAchievements[i]);
        }

        contentRect = content.GetComponent<RectTransform>();
        detailViewHeight = achievementDetailView.GetComponent<RectTransform>().rect.height;

        foreach (var view in detailView)
            view.CheckAchievementState();

        SetContentHeight(detailView.Count);

        HideAchievementUI();
    }

    public void ViewAchievementUI()
    {
        achievementViewUI.SetActive(true);
    }

    public void HideAchievementUI()
    {
        achievementViewUI.SetActive(false);
    }

    public void SetContentHeight(int viewCount)
    {
        
    }

    // 토글 값에따라 업적 보여주는 값 변경
    public void ChangeAchievementState(AchievementViewState state)
    {
        foreach (var achievement in detailView)
        {
            achievement.gameObject.SetActive(true);
            achievement.CheckAchievementState();
        }
        
        if(state != AchievementViewState.All)
        {
            bool isCompleted;

            if (state == AchievementViewState.Running)
                isCompleted = false;
            else
                isCompleted = true;

            foreach (var achievement in detailView)
            {
                if (achievement.IsCompled != isCompleted)
                {
                    achievement.gameObject.SetActive(false);
                }
            }
        }
    }

    private void AddAchievement(Quest _achievement)
    {
        AchievementDetailView newDetailView = Instantiate(achievementDetailView, content.transform);
        newDetailView.SetAchievement(_achievement);
        detailView.Add(newDetailView);
    }
}
