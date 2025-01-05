using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDetailView : MonoBehaviour
{
    [Header("Info")]
    [SerializeField]
    private EquipmentItem equipmentItem;
    [SerializeField]
    private Quest achievement;

    [Header("Viewer")]
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI rewards;
    [SerializeField]
    private Image completedImage;

    [Header("Object")]
    [SerializeField]
    private AchievementTaskView taskView;

    public bool IsCompled => Manager.Instance.Quest.ContainsCompletedAchievements(achievement);
    public void SetAchievement(Quest _achivement)
    {
        achievement = _achivement;

        image.sprite = achievement.Icon;
        title.text = achievement.DisplayName;
        description.text = achievement.Description;
        rewards.text = "";

        taskView.SetAchievementTaskView(achievement.CurrentTaskGroup.Tasks[0]);

        foreach (var reward in achievement.Rewards)
        {
            if(reward is EquipmentAchievementReward equipmentReward)
            {
                rewards.text += $"장비 아이템 [{equipmentReward.UnLockItemName}] 해금";
            }
            else if(reward is InvenItemReward invenReward)
            {
                rewards.text += $"아이템 {invenReward.Item.itemName} {invenReward.Count}개 획득";
            }
            else
            {
                rewards.text += $"아이템 {reward.Description} 획득";
            }   
        }
    }

    public void CheckAchievementState()
    {
        bool isView = IsCompled ? true : false;

        completedImage.gameObject.SetActive(isView);

        if (!achievement.IsAcceptable)
            this.gameObject.SetActive(false);
    }
}