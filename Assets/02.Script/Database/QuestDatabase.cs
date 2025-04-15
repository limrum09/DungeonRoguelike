using UnityEngine;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "GameManager/Database/QuestDatabase")]
public class QuestDatabase : GameDatabase<Quest>
{
#if UNITY_EDITOR
    [ContextMenu("RefreshDatabase")]
    private void RefreshDatabaseInChild()
    {
        RefreshDatabase(); // 부모 클래스의 메서드 호출
    }
#endif
}
