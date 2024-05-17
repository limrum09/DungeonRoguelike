using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelEvent : MonoBehaviour
{
    public Animator settingsPanelAnimator;
    public Animator lobbyPanelAnimator;

    private bool setCharactetInfo;
    private bool setSetting;


    private void Start()
    {
        setCharactetInfo = false;
        setSetting = false;
    }

    public void SetChracetersPanel()
    {
        setCharactetInfo = !setCharactetInfo;

        lobbyPanelAnimator.SetBool("SetOn", setCharactetInfo);
    }

    public void CancelCharacterPanel()
    {
        setCharactetInfo = false;

        lobbyPanelAnimator.SetBool("SetOn", setCharactetInfo);
    }

    public void SetSettingPanel()
    {
        setSetting = !setSetting;

        settingsPanelAnimator.SetBool("SetOn", setSetting);
    }
    public void CancelSettingPanel()
    {
        setSetting = false;

        settingsPanelAnimator.SetBool("SetOn", setSetting);
    }
}
