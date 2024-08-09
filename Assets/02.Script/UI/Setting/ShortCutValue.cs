using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutValue : MonoBehaviour
{
    [SerializeField]
    private InputField inputKeyCodeValue;
    [SerializeField]
    private string keyValue;
    [SerializeField]
    private string prevTextValue;

    public InputField InputKeyCodeValue => inputKeyCodeValue;
    public string KeyValue => keyValue;
    public string PrevTextValue => prevTextValue;

    public void StartShortCutKey()
    {
        string keyCode = Manager.Instance.Key.GetKeyCode(KeyValue).ToString();
        inputKeyCodeValue.text = keyCode;
    }

    public bool CheckLength()
    {
        bool oneChar = false;
        string currentString = inputKeyCodeValue.text;

        // 입력된 값이 1글자일 경우
        if (currentString.Length == 1)
        {
            oneChar = true;

            // 소문자일 경우, 대문자로 변경
            if (currentString[0] >= 'a' && currentString[0] <= 'z')
            {
                currentString = currentString.ToUpper();
                inputKeyCodeValue.text = currentString;
            }
        }
        else if (currentString == KeyCode.Space.ToString())
            oneChar = true;

        return oneChar;
    }

    // 이전 문자 저장
    public void SetPrevText()
    {
        prevTextValue = inputKeyCodeValue.text;
    }

    public void CancelChange()
    {
        inputKeyCodeValue.text = prevTextValue;
    }
}
