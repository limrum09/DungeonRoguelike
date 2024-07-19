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
                HideUI(inventoryUI);
            }
            else
            {
                ViewUI(inventoryUI, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (statusUI.activeSelf)
            {
                HideUI(statusUI);
            }
            else
            {
                ViewUI(statusUI, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (questViewUI.activeSelf)
            {
                HideUI(questViewUI);
            }
            else
            {
                ViewUI(questViewUI, true);
            }
        }
    }

    public void ViewUI(GameObject viewUI, bool setFirst)
    {
        if (setFirst)
        {
            var root = viewUI.transform.parent;
            root.transform.SetAsLastSibling();
        }
        SoundManager.instance.SetAudioAudioPath(SelectAudio.UIOpen, "UI/UI_OpenAndClose");

        viewUI.SetActive(true);
    }

    public void HideUI(GameObject hideUI)
    {
        hideUI.SetActive(false);

        SoundManager.instance.SetAudioAudioPath(SelectAudio.UIClose, "UI/UI_OpenAndClose");
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
