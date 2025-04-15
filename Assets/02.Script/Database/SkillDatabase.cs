using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDatabase", menuName = "GameManager/Database/SkillDatabase")]
public class SkillDatabase : GameDatabase<ActiveSkill>
{
    public void ResetSkillData()
    {
        foreach (var data in datas)
        {
            if (data is ActiveSkill)
                data.ResetSkill();
        }
    }

#if UNITY_EDITOR
    [ContextMenu("RefreshDatabase")]
    private void RefreshDatabaseInChild()
    {
        RefreshDatabase(); // 부모 클래스의 메서드 호출
    }
#endif
}