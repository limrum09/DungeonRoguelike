using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingContorller : MonoBehaviour
{
    [SerializeField]
    private List<ShortCutValue> shortCut;
    [SerializeField]
    private List<SliderValue> soundSliders;
    [SerializeField]
    private RectTransform contentRect;

    public IReadOnlyList<SliderValue> SoundSliders => soundSliders;

    public void SettingControllerStart()
    {
        for(int i = 0; i< shortCut.Count; i++)
        {
            shortCut[i].SetPrevText();
            shortCut[i].StartShortCutKey();
        }

        for (int i = 0; i < soundSliders.Count; i++)
            Manager.Instance.Sound.SetSoundSlider(soundSliders[i]);
    }
    public void ChangeSliderValue(SliderValue slider)
    {
        AudioType getAudioType = slider.Audio;
        MasterVolumeType masterVolumeType = slider.MasterVolume;
        float volume = slider.Slider;

        var sound = Manager.Instance.Sound;

        if (getAudioType == AudioType.AudioTypeLastValue)
        {
            sound.SetMasterVolume(masterVolumeType, volume);
        }
        else
        {
            sound.SetAudioVolume(getAudioType, volume);
        }
    }

    public void ValueChange(string str)
    {
        Debug.Log(str);
    }

    public void ChangeShoryKeyValue(ShortCutValue shortCut)
    {
        if (!shortCut.CheckLength())
        {
            shortCut.CancelChange();
            return;
        }

        string keyCodeString = shortCut.InputKeyCodeValue.text;
        KeyCode _keyCode;

        try
        {
            // 숫자 0 ~ 9 사이의 값일 경우, 'Alpha'를 추가한다.
            if (keyCodeString[0] >= '0' && keyCodeString[0] <= '9')
                keyCodeString = "Alpha" + keyCodeString;

            // Attempt to parse the string to a KeyCode
            _keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyCodeString, true);

            // keyCode 값을 보내고 중복이 있으면 true를 중복이 없으면 false를 반환하여, false인 경우 값을 바꾼다.
            if (!Manager.Instance.Key.ChangKeycode(shortCut.KeyValue, _keyCode))
            {
                Manager.Instance.UIAndScene.ChangeShortCutValue(shortCut.KeyValue);

                shortCut.SetPrevText();
            }
            else
            {
                shortCut.CancelChange();
            }
            
        }
        catch (System.ArgumentException)
        {
            shortCut.CancelChange();
        }
    }

    // 소리, 단축키 등 바로가기 버튼
    public void ClickButtonToPosition(float positionY)
    {
        Vector2 rectPosition = contentRect.anchoredPosition;
        rectPosition.y = positionY;

        contentRect.anchoredPosition = rectPosition;
    }
}
