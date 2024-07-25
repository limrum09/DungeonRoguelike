using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKey : MonoBehaviour
{
    private Dictionary<string, KeyCode> inputKeys;
    // Start is called before the first frame update
    public void InputKeyStart()
    {
        inputKeys = new Dictionary<string, KeyCode>();

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

        inputKeys.Add("ShortKey1", KeyCode.Alpha1);
        inputKeys.Add("ShortKey2", KeyCode.Alpha2);
        inputKeys.Add("ShortKey3", KeyCode.Alpha3);
        inputKeys.Add("ShortKey4", KeyCode.Alpha4);
        inputKeys.Add("ShortKey5", KeyCode.Alpha5);
        inputKeys.Add("ShortKey6", KeyCode.Alpha6);
        inputKeys.Add("ShortKey7", KeyCode.Alpha7);
        inputKeys.Add("ShortKey8", KeyCode.Alpha8);
    }

    public KeyCode GetKeyCode(string keyString)
    {
        return inputKeys[keyString];
    }
}
