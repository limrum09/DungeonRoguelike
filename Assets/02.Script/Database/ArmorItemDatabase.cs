using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName ="ArmorItemDatabase", menuName ="GameManager/Database/ArmorItemDatabase")]
public class ArmorItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<ArmorItem> armorItems;

    public IReadOnlyList<ArmorItem> ArmorItems => armorItems;

    public ArmorItem FindItemBy(string armorItemCodeName) => armorItems.FirstOrDefault(x => x.ItemCode == armorItemCodeName);

    [ContextMenu("FindArmorItem")]
    private void FindArmorItem()
    {
        armorItems = new List<ArmorItem>();

        // ã�� ���� ������ Ÿ��
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(ArmorItem)}");

        foreach(var guid in guids)
        {
            // ã������ ������Ʈ�� ���
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // ��θ� ���� ã�� ������Ʈ
            var armorItem = AssetDatabase.LoadAssetAtPath<ArmorItem>(assetPath);

            if(armorItem.GetType() == typeof(ArmorItem))
            {
                armorItems.Add(armorItem);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

        }
    }
}
