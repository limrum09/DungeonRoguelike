using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardContentSize : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private RectTransform contentSize;

    private void Start()
    {
        UpdateRewardContentSize();
    }

    public void UpdateRewardContentSize()
    {
        float widthSize = 5.0f;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (content.transform.GetChild(i).gameObject.activeSelf)
                widthSize += 70f;
        }

        contentSize.sizeDelta = new Vector2(widthSize, contentSize.sizeDelta.y);
    }
}
