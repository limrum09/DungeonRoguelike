using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementDatabase", menuName = "GameManager/Database/AchievementDatabase")]
public class AchievementDatabase : GameDatabase<Achievement>
{
#if UNITY_EDITOR
    [ContextMenu("RefreshDatabase")]
    private void RefreshDatabaseInChild()
    {
        RefreshDatabase(); // 부모 클래스의 메서드 호출
    }
#endif
}
