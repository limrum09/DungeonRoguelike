using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKey : MonoBehaviour
{
    private Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>();
    // Start is called before the first frame update
    public void InputKeyStart()
    {
        ResetKeyCode();

        Debug.Log("Reset Key");
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

        }


        inputKeys[keyString] = code;

        // 저장 필요
    }

    public KeyCode GetKeyCode(string keyString)
    {
        return inputKeys[keyString];
    }
}
