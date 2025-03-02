using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Scriptable Object/SceneAsset")]
public class SceneInfomation : ScriptableObject
{
    [SerializeField]
    private string sceenName;
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
    public string SceneName => sceenName;
}
