using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DongeonEnterButton : MonoBehaviour
{
    private Button btn;
    private string sceneName;
    // Start is called before the first frame update
    public void EnterBuuttonStart(string _sceneName)
    {
        sceneName = _sceneName;
        btn = GetComponent<Button>();

        btn.onClick.AddListener(EneterScene);
    }

    public void ButtonInteractable(bool value)
    {
        btn.interactable = value;
    }

    private void EneterScene()
    {
        if (sceneName == null)
            return;
        Manager.Instance.UIAndScene.LoadScene(sceneName);
    }
}
