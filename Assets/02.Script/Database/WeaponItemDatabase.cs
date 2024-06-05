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

        // ã�� ���� ������ Ÿ��
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(WeaponItem)}");

        foreach (var guid in guids)
        {
            // ã������ ������Ʈ�� ���
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // ��θ� ���� ã�� ������Ʈ
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
