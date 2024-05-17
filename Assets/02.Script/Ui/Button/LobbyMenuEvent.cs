using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenuEvent : MonoBehaviour
{
    public Animator settingAnimator;
    public Animator charaterAnimator;
    public Animator gameStartAnimator;

    private bool setOn;

    private void Start()
    {
        setOn = false;
    }

    public void SetOn()
    {
        if (setOn)
        {
            setOn = false;
        }
        else
        {
            setOn = true;
        }
    }

    public void SettingAnimtor()
    {
        settingAnimator.SetBool("SetOn", setOn);
    }

    public void CharacterAnimtor()
    {
        charaterAnimator.SetBool("SetOn", setOn);
    }

    public void GameStartAnimtor()
    {
        gameStartAnimator.SetBool("SetOn", setOn);
    }
}
