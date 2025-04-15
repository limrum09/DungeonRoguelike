using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputKey : MonoBehaviour
{
    private Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>();

    private bool IsOverlap(string keyString, KeyCode code)
    {
        foreach(var keyValue in inputKeys)
        {
            if (keyValue.Key != keyString && keyValue.Value == code)
                return true;
        }

        return false;
    }

    public void InputKeyStart()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveFile");

        if (!File.Exists(path))
        {
            ResetKeyCode();

            Debug.Log("Don't have load file, Reset Key");
        }
    }

    public void ResetKeyCode()
    {
        inputKeys.Clear();

        Debug.Log("키 리셋");

        inputKeys.Add("Inventory", KeyCode.I);
        inputKeys.Add("Skill", KeyCode.K);
        inputKeys.Add("Quest", KeyCode.Q);
        inputKeys.Add("Status", KeyCode.F);
        inputKeys.Add("Option", KeyCode.Escape);
        inputKeys.Add("Camera", KeyCode.C);

        inputKeys.Add("Attack", KeyCode.Z);
        inputKeys.Add("Sprint", KeyCode.V);
        inputKeys.Add("ToNPC", KeyCode.X);
        inputKeys.Add("Jump", KeyCode.Space);

        inputKeys.Add("ShortKey1", KeyCode.Alpha1);
        inputKeys.Add("ShortKey2", KeyCode.Alpha2);
        inputKeys.Add("ShortKey3", KeyCode.Alpha3);
        inputKeys.Add("ShortKey4", KeyCode.Alpha4);
        inputKeys.Add("ShortKey5", KeyCode.Alpha5);
        inputKeys.Add("ShortKey6", KeyCode.Alpha6);
        inputKeys.Add("ShortKey7", KeyCode.Alpha7);
        inputKeys.Add("ShortKey8", KeyCode.Alpha8);

        Debug.Log("Count : " + inputKeys.Count);
    }

    public bool ChangKeycode(string keyString, KeyCode code)
    {
        if (IsOverlap(keyString, code))
            return true;

        bool isNotWASD = (code != KeyCode.W && code != KeyCode.A && code != KeyCode.S && code != KeyCode.D);
        if ((code >= KeyCode.A && code <= KeyCode.Z && isNotWASD) || 
            (code >= KeyCode.Alpha1 && code <= KeyCode.Alpha9) || code == KeyCode.Space)
        {
            inputKeys[keyString] = code;
        }

        return false;
        // 저장 필요
    }

    public KeyCode GetKeyCode(string keyString, bool isUI = false)
    {
        if (!Manager.Instance.canInputKey && !isUI)
        {
            return KeyCode.None;
        }

        if (inputKeys.TryGetValue(keyString, out KeyCode keyCode))
        {
            return keyCode;
        }
        else
        {
            Debug.LogWarning("KeyCode not found for key: " + keyString);
            // 필요에 따라 처리해도됨
            return KeyCode.None;
        }
    }

    // Json 저장을 위한 직열화
    public string SerializeShortCutKeyDictionary()
    {
        return JsonConvert.SerializeObject(inputKeys);
    }

    // 직열화를 해서 저장한 데이터를 가져오기
    public void DeserializeShortCutKeyDictionary(string json)
    {
        var deserializedInputKeys = JsonConvert.DeserializeObject<Dictionary<string, KeyCode>>(json);

        // key-value pair가 올바르게 저장되어 있는지 확인
        if (deserializedInputKeys != null && deserializedInputKeys.Count > 0)
        {
            inputKeys = deserializedInputKeys;
        }
        else
        {
            ResetKeyCode();
        }
    }
}
