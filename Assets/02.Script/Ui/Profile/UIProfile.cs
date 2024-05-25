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

    public void SetHPBar()
    {
        var player = PlayerStatus.instance;

        int maxHP = player.MaxHP;
        int currentHP = player.CurrentHP;

        maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString();

        currentHPBar.rectTransform.sizeDelta = new Vector2((float)currentHP/ (float)maxHP * 230f, currentHPBar.rectTransform.sizeDelta.y);
    }

    public void SetExpBar()
    {
        var player = PlayerStatus.instance;

        int level = player.Level;
        int maxEXP = player.Exp;
        int currentEXP = player.CurrentExp;

        levelText.text = level.ToString();
        maxEXPText.text = maxEXP.ToString();
        currentEXPText.text = currentEXP.ToString();

        currentExpBar.rectTransform.sizeDelta = new Vector2((float)currentEXP / (float)maxEXP * 230f, currentExpBar.rectTransform.sizeDelta.y);
    }
}
