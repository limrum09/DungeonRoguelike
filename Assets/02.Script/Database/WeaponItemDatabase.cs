using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemDatabase", menuName = "GameManager/Database/WeaponItemDatabase")]
public class WeaponItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<WeaponItem> weaponItem;

    public IReadOnlyList<WeaponItem> WeaponItems => weaponItem;

    [ContextMenu("FindWeaponItem")]
    private void FindArmorItem()
    {
        weaponItem = new List<WeaponItem>();

        // 찾고 싶은 에셋의 타입
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(WeaponItem)}");

        foreach (var guid in guids)
        {
            // 찾으려는 오브젝트의 경로
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // 경로를 통해 찾은 오브젝트
            var armorItem = AssetDatabase.LoadAssetAtPath<WeaponItem>(assetPath);

            if (armorItem.GetType() == typeof(WeaponItem))
            {
                weaponItem.Add(armorItem);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}
