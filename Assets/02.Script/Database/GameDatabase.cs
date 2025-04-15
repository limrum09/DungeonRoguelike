using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class GameDatabase<T> : ScriptableObject where T : GameDataObject
{
    [SerializeField]
    protected List<T> datas;

    public IReadOnlyList<T> Datas => datas;
    public T FindByCode(string code) => datas.FirstOrDefault(x => x.Code == code);

#if UNITY_EDITOR
    public void RefreshDatabase()
    {
        datas = new List<T>();
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var data = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (data.GetType() == typeof(T))
                datas.Add(data);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
#endif
}
