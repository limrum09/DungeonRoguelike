using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName ="ArmorItemDatabase", menuName ="GameManager/Database/ArmorItemDatabase")]
public class ArmorItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<ArmorItem> armorItems;

    public IReadOnlyList<ArmorItem> ArmorItems => armorItems;

    public ArmorItem FindItemBy(string armorItemCodeName) => armorItems.FirstOrDefault(x => x.ItemCode == armorItemCodeName);

#if UNITY_EDITOR
    [ContextMenu("FindArmorItem")]
    private void FindArmorItem()
    {
        armorItems = new List<ArmorItem>();

        // 찾고 싶은 에셋의 타입 
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(ArmorItem)}");

        foreach (var guid in guids)
        {
            // 찾으려는 오브젝트의 경로
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // 경로를 통해 찾은 오브젝트
            var armorItem = AssetDatabase.LoadAssetAtPath<ArmorItem>(assetPath);

            if (armorItem.GetType() == typeof(ArmorItem))
            {
                armorItems.Add(armorItem);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

        }
    }
#endif
}
