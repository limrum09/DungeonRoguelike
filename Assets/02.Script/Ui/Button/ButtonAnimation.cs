using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    private Animator btnAnimator;

    // Start is called before the first frame update
    void Start()
    {
        btnAnimator = GetComponent<Animator>();
    }

    public void LobbyMenuClick()
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
