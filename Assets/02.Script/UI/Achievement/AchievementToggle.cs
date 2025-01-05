using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementToggle : MonoBehaviour
{
    [SerializeField]
    private AchievementViewState state;
    [SerializeField]
    private AchievementUI achievementUI;

    public void OnValueChage(bool _bool)
    {
        achievementUI.ChangeAchievementState(state);
    }
}
