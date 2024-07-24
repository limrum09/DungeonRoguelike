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

    public void SoundManagerStart()
    {
        var root = this.gameObject;

        string[] soundsName = System.Enum.GetNames(typeof(SelectAudio));

        audios = new AudioSource[(int)SelectAudio.AudioCount];
        audioClips = new Dictionary<string, AudioClip>();

        for (int i = 0; i < soundsName.Length - 1; i++)
        {
            GameObject newSoundObject = new GameObject();
            newSoundObject.name = soundsName[i];
            newSoundObject.transform.SetParent(root.transform, false);

            audios[i] = newSoundObject.AddComponent<AudioSource>();
        }

        if (bgmClips != null)
        {

        }
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
        if (sceneName.Contains("Sound/") == false)
            sceneName = $"Sound/{sceneName}";

        AudioClip bgmClip = Resources.Load<AudioClip>(sceneName);

        bgmAudio.clip = bgmClip;
        bgmAudio.Play();
    }
    public void SetAudioAudioPath(SelectAudio audio, string clipPath)
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

        if (audioType == SelectAudio.PlayerFoot)
            playAudio.pitch = PlayerInteractionStatus.instance.PlayerSpeed;

        playAudio.PlayOneShot(audioClip);
    }
}
