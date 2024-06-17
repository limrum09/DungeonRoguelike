using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionTextController : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI descriptionText;
    [SerializeField]
    public RectTransform content;         // TextMeshPro를 포함하고 있는 Content
    private void Start()
    {
        UpdateContainerHeight();
    }

    public void UpdateContainerHeight()
    {
        // 호출 된 당시의 TextMeshPro의 높이
        float textHeight = descriptionText.preferredHeight;

        // Content의 길이는 그대로 사용하고, 높이만 변경
        content.sizeDelta = new Vector2(content.sizeDelta.x, textHeight);
    }
}
