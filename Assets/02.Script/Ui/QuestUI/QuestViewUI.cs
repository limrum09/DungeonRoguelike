using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestViewUI : MonoBehaviour
{
    [SerializeField]
    private GameObject questViewUI;
    [SerializeField]
    private QuestListViewController listView;
    [SerializeField]
    private QuestDetailViewController detailView;

    

    private void Start()
    {
        listView.ListViewStart();
        detailView.DetailViewStart();
    }
}
