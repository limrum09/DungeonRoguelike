using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buffName;
    [SerializeField]
    private TextMeshProUGUI healInfo;
    [SerializeField]
    private TextMeshProUGUI damageInfo;
    [SerializeField]
    private TextMeshProUGUI speedInfo;

    [SerializeField]
    private RectTransform rect;

    private string healInfoStr;
    private string damageInfoStr;
    private string speedInfoStr;

    public void ViewBuffInfo(string getBuffName, int getHPHealValue, float hpHealTime, float getDamageValue, float getSpeedValue)
    {
        float rectHeight = 0.0f;

        buffName.text = getBuffName;

        if (!string.IsNullOrEmpty(getBuffName))
        {
            buffName.text = getBuffName;
            rectHeight = 35.0f;
        }
        else
        {
            gameObject.SetActive(false);
            return;
        }
            

        healInfoStr = string.Empty;
        damageInfoStr = string.Empty;
        speedInfoStr = string.Empty;

        if (getHPHealValue != 0)
        {
            if (getHPHealValue > 0)
                healInfoStr = $"<color=green>{hpHealTime}</color>초마다 체력 + <color=green>{getHPHealValue}</color>";
            else if (getHPHealValue < 0)
                healInfoStr = $"<color=red>{hpHealTime}</color>초마다 체력 - <color=red>{getHPHealValue}</color>";

            rectHeight += 20.0f;
        }           

        if(getDamageValue != 0)
        {
            if (getDamageValue > 0)
                damageInfoStr = $"공격력 증가 + <color=green>{getDamageValue}</color>";
            else if (getDamageValue < 0)
                damageInfoStr = $"공격력 감소 + <color=red>{getDamageValue}</color>";

            rectHeight += 20.0f;
        }

        if (getSpeedValue != 0)
        {
            if (getSpeedValue > 0)
                speedInfoStr = $"이동속도 증가 + <color=green>{getSpeedValue}</color>";
            else if (getSpeedValue < 0)
                speedInfoStr = $"이동속도 감소 + <color=red>{getSpeedValue}</color>";

            rectHeight += 20.0f;
        }

        if (!string.IsNullOrEmpty(healInfoStr) || !string.IsNullOrEmpty(damageInfoStr) || !string.IsNullOrEmpty(speedInfoStr))
            rectHeight += 20.0f;

        healInfo.text = healInfoStr;
        damageInfo.text = damageInfoStr;
        speedInfo.text = speedInfoStr;
        
        rect.sizeDelta = new Vector2(rect.rect.width, rectHeight);
    }
}
