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

    [SerializeField]
    private DongeonCondition dongeonCondition;
    
    // Start is called before the first frame update
    public void DongeonPanelStart()
    {
        if(sceneInfo != null)
        {
            dongeonImage.sprite = sceneInfo.SceneInfoSprite;
            dongeonTitleText.text = sceneInfo.SceneTitle;
            dongeonInfoText.text = sceneInfo.SceneInfo;
            enterButton.EnterBuuttonStart(sceneInfo.SceneName);

            bool btnInteractable = true;
            if(dongeonCondition != null)
            {
                string conditionText = string.Empty;
                conditionText += "\n";

                if (!dongeonCondition.IsLevelPass())
                {
                    conditionText += $"\n<color=red>레벨 {dongeonCondition.LevelCondition}이상 입장가능</color>";
                    btnInteractable = false;
                }

                if (!dongeonCondition.IsLevelPass())
                {
                    conditionText += $"\n<color=red>업적 : [{dongeonCondition.AchievementCondition}] 완료 후 입장가능</color>";
                    btnInteractable = false;
                }

                dongeonInfoText.text += conditionText;
            }

            enterButton.ButtonInteractable(btnInteractable);
                
        }
    }
}
