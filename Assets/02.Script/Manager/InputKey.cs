using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputKey : MonoBehaviour
{
    private Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>();
    // Start is called before the first frame update
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

        inputKeys.Add("Inventory", KeyCode.I);
        inputKeys.Add("Skill", KeyCode.K);
        inputKeys.Add("Quest", KeyCode.Q);
        inputKeys.Add("Status", KeyCode.F);
        inputKeys.Add("Option", KeyCode.Escape);

        inputKeys.Add("ShortKey1", KeyCode.Alpha1);
        inputKeys.Add("ShortKey2", KeyCode.Alpha2);
        inputKeys.Add("ShortKey3", KeyCode.Alpha3);
        inputKeys.Add("ShortKey4", KeyCode.Alpha4);
        inputKeys.Add("ShortKey5", KeyCode.Alpha5);
        inputKeys.Add("ShortKey6", KeyCode.Alpha6);
        inputKeys.Add("ShortKey7", KeyCode.Alpha7);
        inputKeys.Add("ShortKey8", KeyCode.Alpha8);
    }

    public void ChangKeycode(string keyString, KeyCode code)
    {
        if((code >= KeyCode.A && code <= KeyCode.Z && code != KeyCode.W && code != KeyCode.A && code != KeyCode.S && code != KeyCode.D) || 
            (code >= KeyCode.Alpha1 && code <= KeyCode.Alpha9))
        {
            inputKeys[keyString] = code;
        }

        // 저장 필요
    }

    public KeyCode GetKeyCode(string keyString)
    {
        return inputKeys[keyString];
    }

    // Json 저장을 위한 직열화
    public string SerializeShortCutKeyDictionary()
    {
        return JsonConvert.SerializeObject(inputKeys);
    }

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
