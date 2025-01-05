using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSelectButton : EquipmentSelectButton
{
    [SerializeField]
    private WeaponItem weaponItem;

    private bool completedCondition = false;

    protected override void Start()
    {
        base.Start();

        if(weaponItem.Condition != null)
            if (!weaponItem.Condition.IsAchievementPass && Manager.Instance?.Quest != null)
            {
                completedCondition = false;
                Manager.Instance.Quest.onAchievementCompleted -= CheckAchivementCondition;
                Manager.Instance.Quest.onAchievementCompleted += CheckAchivementCondition;

                btn.interactable = weaponItem.Condition.IsAchievementPass;
            }
    }

    private void CheckAchivementCondition(Quest quest)
    {
        bool pass = weaponItem.Condition.IsAchievementPass;
        btn.interactable = pass;

        if (pass)
        {
            completedCondition = true;
            Manager.Instance.Quest.onAchievementCompleted -= CheckAchivementCondition;
        }            
    }

    private void OnDestroy()
    {
        if(!completedCondition)
            Manager.Instance.Quest.onAchievementCompleted -= CheckAchivementCondition;
    }

    protected override void OnClickedButton()
    {
        Manager.Instance.UIAndScene.ChangeEquipment(weaponItem);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Manager.Instance.UIAndScene.EquipmentSelectUI.ViewItemInfo(0, weaponItem.ItemCode, this.transform, rectTf.rect);
    }
}
