using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audio;

    public void Init()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayAudioOneShot(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
