using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmorSelectButton : EquipmentSelectButton
{
    [SerializeField]
    private ArmorItem armorItem;

    private bool completedCondition = false;

    protected override void Start()
    {
        base.Start();

        if (!armorItem.Condition.IsAchievementPass && Manager.Instance?.Quest != null)
        {
            completedCondition = false;
            Manager.Instance.Quest.onAchievementCompleted -= CheckAchivementCondition;
            Manager.Instance.Quest.onAchievementCompleted += CheckAchivementCondition;

            btn.interactable = armorItem.Condition.IsAchievementPass;
        }
    }

    private void CheckAchivementCondition(Quest quest)
    {
        bool pass = armorItem.Condition.IsAchievementPass;
        btn.interactable = pass;

        if (pass)
        {
            completedCondition = true;
            Manager.Instance.Quest.onAchievementCompleted -= CheckAchivementCondition;
        }
    }

    private void OnDestroy()
    {
        if (!completedCondition)
            Manager.Instance.Quest.onAchievementCompleted -= CheckAchivementCondition;
    }

    protected override void OnClickedButton()
    {
        Manager.Instance.UIAndScene.ChangeEquipment(armorItem);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Manager.Instance.UIAndScene.EquipmentSelectUI.ViewItemInfo(1, armorItem.ItemCode, this.transform, rectTf.rect);
    }
}
