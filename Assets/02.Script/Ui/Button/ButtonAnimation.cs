using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    private Animator btnAnimator;
    private Button button;
    private LobbyMenuEvent menuEvent;

    // Start is called before the first frame update
    void Start()
    {
        btnAnimator = GetComponent<Animator>();
        button = GetComponent<Button>();
        menuEvent = GetComponent<LobbyMenuEvent>();

        button.onClick.AddListener(LobbyMenuButtonClick);
    }

    public void LobbyMenuButtonClick()
    {
        LobbyMenuClick();
        menuEvent.SetOn();
        menuEvent.GameStartAnimtor();
        menuEvent.CharacterAnimtor();
        menuEvent.SettingAnimtor();
    }

    private void LobbyMenuClick()
    {
        bool menuButtonClick = btnAnimator.GetBool("MenuButtonClick");

        if(!menuButtonClick)
        {
            btnAnimator.SetBool("MenuButtonClick", true);
        }
        else
        {
            btnAnimator.SetBool("MenuButtonClick", false);
        }        
    }
}
