using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DongeonPanelController : MonoBehaviour
{
    [SerializeField]
    private SceneInfomation sceneInfo;
    [SerializeField]
    private Image dongeonImage;
    [SerializeField]
    private TextMeshProUGUI dongeonTitleText;
    [SerializeField]
    private TextMeshProUGUI dongeonInfoText;
    [SerializeField]
    private DongeonEnterButton enterButton;
    
    // Start is called before the first frame update
    public void DongeonPanelStart()
    {
        if(sceneInfo != null)
        {
            dongeonImage.sprite = sceneInfo.SceneInfoSprite;
            dongeonTitleText.text = sceneInfo.SceneTitle;
            dongeonInfoText.text = sceneInfo.SceneInfo;
            enterButton.EnterBuuttonStart(sceneInfo.SceneName);
        }
    }
}
