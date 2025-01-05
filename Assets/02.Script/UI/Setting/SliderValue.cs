using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float Slider => slider.value;

    public SoundSliderData SoundValueSave()
    {
        return new SoundSliderData
        {
            masterType = this.masterVolume.ToString(),
            audioType = this.audioType.ToString(),
            value = this.slider.value
        };
    }

    public void SetSldierValueToLoad(float setValue)
    {
        slider.value = setValue;
    }
}
