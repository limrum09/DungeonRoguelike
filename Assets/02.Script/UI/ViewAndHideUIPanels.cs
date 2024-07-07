using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAndHideUIPanels : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    GameObject lobbyUI;
    [SerializeField]
    GameObject shortcutkeyUI;
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    GameObject questViewUI;
    [SerializeField]
    GameObject NPCUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.activeSelf)
            {
                HideInventory();
            }
            else
            {
                ViewInventory();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (statusUI.activeSelf)
            {
                HideStatus();
            }
            else
            {
                ViewStatus();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (questViewUI.activeSelf)
            {
                HideQuestUI();
            }
            else
            {
                ViewQuestUI();
            }
        }
    }

    public void ViewInventory()
    {
        inventoryUI.transform.SetAsFirstSibling();
        inventoryUI.SetActive(true);
    }
    public void HideInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void ViewStatus()
    {
        statusUI.transform.SetAsFirstSibling();
        statusUI.SetActive(true);
    }
    public void HideStatus()
    {
        statusUI.SetActive(false);
    }

    public void ViewQuestUI()
    {
        questViewUI.transform.SetAsFirstSibling();
        questViewUI.SetActive(true);
    }
    public void HideQuestUI()
    {
        questViewUI.SetActive(false);
    }

    public void LobbyScene()
    {
        lobbyUI.SetActive(true);
    }
    public void OUTLobbyScene()
    {
        lobbyUI.SetActive(false);
    }
}
