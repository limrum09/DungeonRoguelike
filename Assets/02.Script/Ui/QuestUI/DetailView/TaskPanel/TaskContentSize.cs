using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskContentSize : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private RectTransform contentSize;

    private void Start()
    {
        UpdateTaskContentSize();
    }

    public void UpdateTaskContentSize()
    {
        float heightSize = 0.0f;

        for(int i = 0; i < content.transform.childCount; i++)
        {
            if(content.transform.GetChild(i).gameObject.activeSelf)
                heightSize += 50f;
        }

        contentSize.sizeDelta = new Vector2(contentSize.sizeDelta.x, heightSize);
    }
}
