using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName ="ArmorItemDatabase", menuName ="GameManager/Database/ArmorItemDatabase")]
public class ArmorItemDatabase : GameDatabase<ArmorItem>
{
#if UNITY_EDITOR
    [ContextMenu("RefreshDatabase")]
    private void RefreshDatabaseInChild()
    {
        RefreshDatabase(); // 부모 클래스의 메서드 호출
    }
#endif
}
