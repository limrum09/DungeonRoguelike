using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Scriptable Object/SceneAsset")]
public class SceneInfomation : ScriptableObject
{
    [SerializeField]
    private SceneAsset sceneAsset;
    [SerializeField]
    private Sprite sceneInfoSprite;
    [SerializeField]
    private string sceneTitle;
    [TextArea]
    [SerializeField]
    private string sceneInfo;

    public Sprite SceneInfoSprite => sceneInfoSprite;
    public string SceneTitle => sceneTitle;
    public string SceneInfo => sceneInfo;
    public string SceneName => sceneAsset.name;
}
