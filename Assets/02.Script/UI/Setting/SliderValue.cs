using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MasterVolumeType
{
    None,
    BGM,
    SFX
}
public class SliderValue : MonoBehaviour
{
    [SerializeField]
    private AudioType audioType;
    [SerializeField]
    private MasterVolumeType masterVolume;
    [SerializeField]
    private Slider slider;

    public AudioType Audio => audioType;
    public MasterVolumeType MasterVolume => masterVolume;
    public Slider Slider => slider;
}
