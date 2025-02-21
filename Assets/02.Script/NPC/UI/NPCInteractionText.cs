using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractionText : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI interactionText;
    [SerializeField]
    private NPCTalkUIController npcTalk;

    private void Start()
    {
        PlayerOut();
    }

    public void PlayerIn()
    {
        icon.gameObject.SetActive(true);
        interactionText.gameObject.SetActive(true);
    }

    public void PlayerOut()
    {
        icon.gameObject.SetActive(false);
        interactionText.gameObject.SetActive(false);
        npcTalk.CloseQuestUI();
    }
}
