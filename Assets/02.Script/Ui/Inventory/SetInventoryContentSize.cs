using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetInventoryContentSize : MonoBehaviour
{
    private RectTransform content;
    private RectTransform invenPrefab;

    private int invenLineCount;
    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<RectTransform>();
        invenPrefab = this.transform.GetChild(0).GetComponent<RectTransform>();

        ResetContentSize();
    }

    public void ResetContentSize()
    {
        invenLineCount = content.gameObject.transform.childCount / 4;

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        Vector2 contentSize = content.sizeDelta;
        contentSize.y = (invenPrefab.sizeDelta.y * invenLineCount) + (10f * invenLineCount) + 40f;
        content.sizeDelta = contentSize;
    }
}
