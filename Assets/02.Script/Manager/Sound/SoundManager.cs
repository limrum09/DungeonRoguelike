using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioType
{
    PlayerAttack,
    PlayerFoot,
    PlayerSkill,
    UIClick,
    UIOpen,
    UIClose,
    AudioTypeLastValue
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

        // AudioType의 값들을 soundName에 string값으로 저장
        string[] soundsName = System.Enum.GetNames(typeof(AudioType));

        // audios 배열의 크기를 AudioType의 마지막 값으로 설정
        audios = new AudioSource[(int)AudioType.AudioTypeLastValue];
        audioClips = new Dictionary<string, AudioClip>();

        // AudioType의 개수만큼 GameObject를 만들고 이름을 지정 후, 새로 만들어진 GameObject를 SoundManager의 자식으로 넣기
        // 해당 GameObject에 AudioSource를 추가하고, audios배열에 순서대로 추가
        for (int i = 0; i < soundsName.Length - 1; i++)
        {
            GameObject newSoundObject = new GameObject();
            newSoundObject.name = soundsName[i];
            newSoundObject.transform.SetParent(root.transform, false);

            audios[i] = newSoundObject.AddComponent<AudioSource>();
            audios[i].playOnAwake = false;
        }

        if (bgmClips != null)
        {

        }
    }

    // 전부 초기화
    public void Clear()
    {
        foreach(AudioSource audio in audios)
        {
            audio.clip = null;
            audio.Stop();
        }

        bgmAudio.Stop();
    }

    // BGM 변경
    public void ChaneBGM(string sceneName)
    {
        if (sceneName.Contains("Sound/") == false)
            sceneName = $"Sound/{sceneName}";

        AudioClip bgmClip = Resources.Load<AudioClip>(sceneName);

        bgmAudio.clip = bgmClip;
        bgmAudio.Play();
    }

    // 외부에서 소리 재생시, 소리 종류와 위치를 받음
    public void SetAudioAudioPath(AudioType audio, string clipPath)
    {
        AudioClip playClip = GetAudioClip(clipPath);

        PlayAudio(playClip, audio);
    }

    public void SetAudioVolume(AudioType audioType, float value)
    {
        if(audioType == AudioType.UIClick || audioType == AudioType.UIClose || audioType == AudioType.UIOpen)
        {
            AudioSource playAudio1 = audios[(int)AudioType.UIClick];
            AudioSource playAudio2 = audios[(int)AudioType.UIClose];
            AudioSource playAudio3 = audios[(int)AudioType.UIOpen];

            playAudio1.volume = value * masterVolumeSFX;
            playAudio2.volume = value * masterVolumeSFX;
            playAudio3.volume = value * masterVolumeSFX;
        }
        else
        {
            AudioSource playAudio = audios[(int)audioType];
            playAudio.volume = value * masterVolumeSFX;
        }
    }

    public void SetMasterVolume(MasterVolumeType masterVolume, float value)
    {
        if (masterVolume == MasterVolumeType.BGM)
        {
            masterVolumeBGM = value;
            bgmAudio.volume = masterVolumeBGM;
        }
        else if (masterVolume == MasterVolumeType.SFX)
            masterVolumeSFX = value;
    }

    // 이름과 위치로 AudioClip을 가져옴
    private AudioClip GetAudioClip(string clipPath)
    {
        // clipPath에 Sound가 포함되지 않을 시, 추가
        if (clipPath.Contains("Sound/") == false)
            clipPath = $"Sound/{clipPath}";

        // return해줄 AudioClip을 null값으로 초기화
        AudioClip playClip = null;

        // Dictionary에서 clipPath를 key값으로 AudioClip이 있는지 확인하고, 있다면 playClip에 out
        if (audioClips.TryGetValue(clipPath, out playClip) == false)
        {
            // 저장 된, AudioClip이 없을 경우, clipPath의 위치에 해당하는 AudioClip 가져오기
            playClip = Resources.Load<AudioClip>(clipPath);
            // Dictionary에 추가
            audioClips.Add(clipPath, playClip);
        }

        return playClip;
    }

    // AudioClip과 Audio의 Type을 받고, 해당하는 AudioSource에서 재생
    private void PlayAudio(AudioClip audioClip, AudioType audioType)
    {
        // audios에 AudioSource를 저장할 당시, SelectAudio의 순서에 따라 AudioSource를 Obeject로 저장함.
        AudioSource playAudio = audios[(int)audioType];

        // AudioType이 PlayerFoot일 경우, pitch를 조정하여 재생속도 증가
        if (audioType == AudioType.PlayerFoot)
            playAudio.pitch = PlayerInteractionStatus.instance.PlayerSpeed;

        // 오디오 재생
        playAudio.PlayOneShot(audioClip);
    }
}
