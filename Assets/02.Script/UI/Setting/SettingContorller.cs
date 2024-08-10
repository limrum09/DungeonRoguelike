using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingContorller : MonoBehaviour
{
    [SerializeField]
    private List<ShortCutValue> shortCut;
    [SerializeField]
    private RectTransform contentRect;

    public void SettingControllerStart()
    {
        for(int i = 0; i< shortCut.Count; i++)
        {
            shortCut[i].SetPrevText();
            shortCut[i].StartShortCutKey();
        }
    }
    public void ChangeSliderValue(SliderValue slider)
    {
        AudioType getAudioType = slider.Audio;
        MasterVolumeType masterVolumeType = slider.MasterVolume;
        float volume = slider.Slider.value;

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

        Debug.Log("Key Code String : " + keyCodeString);

        try
        {
            // Attempt to parse the string to a KeyCode
            _keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyCodeString, true);
            Debug.Log("Set Key Code : " + _keyCode);
            
            Manager.Instance.Key.ChangKeycode(shortCut.KeyValue, _keyCode);
            UIAndSceneManager.instance.ChangeShortCutValue(shortCut.KeyValue);

            shortCut.SetPrevText();
        }
        catch (System.ArgumentException)
        {
            Debug.Log("Error String : " + keyCodeString);
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
