using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestReport : MonoBehaviour
{
    [SerializeField]
    private QuestCategory category;
    [SerializeField]
    private TaskTarget target;
    [SerializeField]
    private int successCount;
    [SerializeField]
    private string[] colliderTargets;

    private void OnTriggerEnter(Collider other)
    {
        ReportIfPassCondition(other);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReportIfPassCondition(collision);
    }

    public void ReportToQuest()
    {
        QuestSystem.instance.QuestSystemRecieveReport(category, target, successCount);
    }

    private void ReportIfPassCondition(Component component)
    {
        if(colliderTargets.Any(x => component.CompareTag(x)))
        {
            ReportToQuest();
        }
    }
}
