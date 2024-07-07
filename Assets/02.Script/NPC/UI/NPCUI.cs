using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUI : MonoBehaviour
{
    [SerializeField]
    private NPCInteractionText interactionText;
    [SerializeField]
    private NPCTalkUIController talkUI;

    public NPCInteractionText InteractionText => interactionText;
    public NPCTalkUIController TalkUI => talkUI;
}
