using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "InvenItemDatabase", menuName = "GameManager/Database/InvenItemDatabase")]
public class InvenItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<InvenItem> invenItem;

    public IReadOnlyList<InvenItem> InvenItems => invenItem;

    public InvenItem FindItemBy(string invenItemCodeName) => invenItem.FirstOrDefault(x => x.ItemCode == invenItemCodeName);

    [ContextMenu("FindInvenItem")]
    private void FindInvenItem()
    {
        invenItem = new List<InvenItem>();

        // ã�� ���� ������ Ÿ��
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(InvenItem)}");

        foreach (var guid in guids)
        {
            // ã������ ������Ʈ�� ���
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // ��θ� ���� ã�� ������Ʈ
            var armorItem = AssetDatabase.LoadAssetAtPath<InvenItem>(assetPath);

            if (armorItem.GetType() == typeof(InvenItem))
            {
                invenItem.Add(armorItem);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}
