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
    }

    public void LobbyUIActiveFalse()
    {
        foreach (var panel in buttonUIs)
            panel.SetActive(false);

        lobbyMenuButton.LobbyMenuButtonClick();
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
