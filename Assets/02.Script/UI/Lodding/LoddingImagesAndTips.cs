using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Images&tips", menuName = "Scriptable Object/LoddingImagesAndTips")]
public class LoddingImagesAndTips : ScriptableObject
{
    [SerializeField]
    private List<Sprite> images;
    [SerializeField][TextArea]
    private List<string> tips;

    public IReadOnlyList<Sprite> Images => images;
    public IReadOnlyList<string> Tips => tips;
}
