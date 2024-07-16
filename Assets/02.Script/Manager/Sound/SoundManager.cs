using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SelectAudio
{
    PlayerAttack,
    PlayerFoot,
    PlayerSkill,
    UIClick,
    UIOpen,
    UIClose,
    AudioCount
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    [SerializeField]
    private AudioSource bgmAudio;
    [SerializeField]
    private AudioClip[] bgmClips;

    [SerializeField]
    private AudioSource[] audios;
    [SerializeField]
    private Dictionary<string, AudioClip> audioClips;

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
        //uiClodeAudio.Init();

        if(instance != null)
        {
            var root = instance.gameObject;

            string[] soundsName = System.Enum.GetNames(typeof(SelectAudio));

            audios = new AudioSource[(int)SelectAudio.AudioCount];
            audioClips = new Dictionary<string, AudioClip>();

            for (int i = 0; i< soundsName.Length - 1; i++)
            {
                GameObject newSoundObject = new GameObject();
                newSoundObject.name = soundsName[i];
                newSoundObject.transform.parent = root.transform;

                audios[i] = newSoundObject.AddComponent<AudioSource>();
            }

            if(bgmClips != null)
            {

            }
        }
    }

    public void Init()
    {
        
    }

    public void Clear()
    {
        foreach(AudioSource audio in audios)
        {
            audio.clip = null;
            audio.Stop();
        }

        bgmAudio.Stop();
    }

    public void ChaneBGM(string sceneName)
    {

    }
    public void GetAudioAudioPath(SelectAudio audio, string clipPath)
    {
        AudioClip playClip = GetAudioClip(clipPath);

        PlayAudio(playClip, audio);
    }

    private AudioClip GetAudioClip(string clipPath)
    {
        if (clipPath.Contains("Sound/") == false)
            clipPath = $"Sound/{clipPath}";

        AudioClip playClip = null;

        if (audioClips.TryGetValue(clipPath, out playClip) == false)
        {
            playClip = Resources.Load<AudioClip>(clipPath);
            audioClips.Add(clipPath, playClip);
        }

        return playClip;
    }

    private void PlayAudio(AudioClip audioClip, SelectAudio audioType)
    {
        AudioSource playAudio = audios[(int)audioType];
        playAudio.PlayOneShot(audioClip);
    }
}
