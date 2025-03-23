using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dongeon/Condition/IsDongeonCondition", fileName = "IsDongeonCondition_")]
public class DongeonCondition : ScriptableObject
{
    [SerializeField]
    private int levelCondition;
    [SerializeField]
    private IsAchievementComplete achievementCondition;

    public int LevelCondition => levelCondition;
    public string AchievementCondition {
        get {
            string name = string.Empty;
            if (achievementCondition != null)
                name = achievementCondition.TargetName;

            return name;
        }
    }


    public bool IsLevelPass()
    {
        bool value = true;

        if (levelCondition >= Manager.Instance.Game.Level)
            value = false;

        return value;
    }

    public bool IsAchievementPass()
    {
        bool value = true;

        if (!achievementCondition.IsAchievementPass)
            value = false;

        return value;
    }
}
