using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SelectDongonPanel;
    [SerializeField]
    private GameObject EquipmentSelectPanel;
    [SerializeField]
    private GameObject GameOutPanel;
    [SerializeField]
    private ButtonAnimation lobbyMenuButton;

    [Header("Dongeon Select Panel")]
    [SerializeField]
    private List<DongeonPanelController> dongeons;

    private List<GameObject> buttonUIs;

    public void LobbyUIStart()
    {
        buttonUIs = new List<GameObject>
        {
            SelectDongonPanel,
            EquipmentSelectPanel,
            GameOutPanel
        };

        foreach (var panel in buttonUIs)
            panel.SetActive(false);

        foreach (var dongeon in dongeons)
            dongeon.DongeonPanelStart();
    }

    public void ViewUI(GameObject ui)
    {
        foreach(var panel in buttonUIs)
        {
            if (ui != panel)
                panel.SetActive(false);
            else
            {
                if (panel.activeSelf)
                    panel.SetActive(false);
                else
                    panel.SetActive(true);
            }
                
        }
    }
}
