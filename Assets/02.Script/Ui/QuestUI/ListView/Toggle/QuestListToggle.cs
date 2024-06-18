using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject questListView;
    [SerializeField]
    private Toggle toggle;

    private bool isOn;
    

    public void ToggleSet()
    {
        isOn = toggle.isOn;
    }

    public void SelectToggle()
    {
        isOn = toggle.isOn;

        questListView.SetActive(isOn);
    }
}
