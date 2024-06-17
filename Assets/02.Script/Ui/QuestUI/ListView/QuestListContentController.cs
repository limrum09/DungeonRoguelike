using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListContentController : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private RectTransform contentSize;
    [SerializeField]
    private QuestListContainer containerPrefab;

    // 사용 방법 대충 알겠지만, 정리 필요
    private Dictionary<Quest, GameObject> elemetQuests = new Dictionary<Quest, GameObject>();

    public void AddElement(Quest quest)
    {
        var element = Instantiate(containerPrefab, content.transform);
        element.UpdateQuestList(quest);

        elemetQuests.Add(quest, element.gameObject);
    }

    public void RemoveElement(Quest quest)
    {
        Destroy(elemetQuests[quest]);
        elemetQuests.Remove(quest);
    }

    public void UpdateQuestListContentSize()
    {
        float heightSize = 5.0f;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            heightSize += 43f;
        }

        contentSize.sizeDelta = new Vector2(contentSize.sizeDelta.y, heightSize);
    }
}
