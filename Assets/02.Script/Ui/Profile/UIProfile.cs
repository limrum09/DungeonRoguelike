using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIProfile : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI currentHPText;
    public TextMeshProUGUI maxHPText;
    public TextMeshProUGUI currentEXPText;
    public TextMeshProUGUI maxEXPText;


    public Image currentHPBar;
    public Image currentExpBar;

    public void SetHPBar(int currentHP, int maxHP)
    {
        maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString();

        currentHPBar.rectTransform.sizeDelta = new Vector2((float)currentHP / (float)maxHP * 500f, currentHPBar.rectTransform.sizeDelta.y);
    }

    public void SetExpBar(int level, int currentEXP, int maxEXP)
    {
        levelText.text = level.ToString();
        maxEXPText.text = maxEXP.ToString();
        currentEXPText.text = currentEXP.ToString();

        currentExpBar.rectTransform.sizeDelta = new Vector2((float)currentEXP / (float)maxEXP * 230f, currentExpBar.rectTransform.sizeDelta.y);
    }
}
