using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SelectAudio
{
    PlayerAttack,
    PlayerFoot,
    PlayerSkill,
    UIClick,
    UIClose
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    [SerializeField]
    private AudioSource bgmAudio;
    [SerializeField]
    private AudioClip[] bgmClips;


    [Header("Player Audios")]
    [SerializeField]
    private AudioController playerAttackAudio;
    [SerializeField]
    private List<AudioClip> playerAttackClips;
    [SerializeField]
    private AudioController playerFootAudio;
    [SerializeField]
    private List<AudioClip> playerFootClips;
    [SerializeField]
    private AudioController playerSkillAudio;
    [SerializeField]
    private List<AudioClip> playerSkillClips;

    [Header("UI Audios")]
    [SerializeField]
    private AudioController uiClickAudio;
    [SerializeField]
    private AudioClip uiClickClip;
    [SerializeField]
    private AudioController uiClodeAudio;
    [SerializeField]
    private AudioClip uiCloseClip;

    [Header("Master Volume")]
    public float masterVolumeSFX;
    public float masterVolumeBGM;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("GameManager");
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("GameManager Destroyed");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        
    }

    public void ChaneBGM(string sceneName)
    {

    }

    public void PlayAudio(SelectAudio audio, string audioName)
    {
        switch (audio)
        {
            case SelectAudio.PlayerAttack:
                PlayerAttackAudio(audioName);
                break;
            case SelectAudio.PlayerFoot:
                PlayerFootAudio(audioName);
                break;
            case SelectAudio.PlayerSkill:
                PlayerSkillAudio(audioName);
                break;
            case SelectAudio.UIClick:
                UIClickAudio(audioName);
                break;
            case SelectAudio.UIClose:
                UICloseAudio(audioName);
                break;
        }
    }

    private void PlayerAttackAudio(string audioName)
    {
        int index = 0;

        switch (audioName)
        {
            case "singleSword":
                index = 0;
                break;
            case "doubleSword":
                index = 1;
                break;
            case "swordAndSheid":
                index = 2;
                break;
            case "spear":
                index = 3;
                break;
            case "twoHandSword":
                index = 4;
                break;
            case "magic":
                index = 5;
                break;
        }

        playerAttackAudio.PlayAudioOneShot(playerAttackClips[index]);
    }
    private void PlayerFootAudio(string audioName)
    {

    }
    private void PlayerSkillAudio(string audioName)
    {

    }
    private void UIClickAudio(string audioName)
    {

    }
    private void UICloseAudio(string audioName)
    {

    }
}
