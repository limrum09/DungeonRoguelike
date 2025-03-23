using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject ResurrectionPanel;

    public void PlayerDie()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseDieUI()
    {
        this.gameObject.SetActive(false);
    }
}
