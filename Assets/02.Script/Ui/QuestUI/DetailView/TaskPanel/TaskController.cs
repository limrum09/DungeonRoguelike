using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField]
    private TaskContentSize taskContentSize;

    [SerializeField]
    private GameObject taskGroup;
    [SerializeField]
    private GameObject taskContainer;
    [SerializeField]
    private GameObject content;
    
    public void UpdateTasks(Quest quest)
    {
        

        foreach(var taskGroup in quest.TaskGroups)
        {
            
        }
    }

    private void CreatePool()
    {

    }
}
