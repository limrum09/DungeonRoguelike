using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyData
{
    public Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>();

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
}
