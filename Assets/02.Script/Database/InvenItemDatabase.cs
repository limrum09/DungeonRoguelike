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

        // 찾고 싶은 에셋의 타입
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(InvenItem)}");

        foreach (var guid in guids)
        {
            // 찾으려는 오브젝트의 경로
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // 경로를 통해 찾은 오브젝트
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
