using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIProfile : MonoBehaviour
{
    public static UIProfile instance;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI currentHPText;
    public TextMeshProUGUI maxHPText;
    public TextMeshProUGUI currentEXPText;
    public TextMeshProUGUI maxEXPText;


    public Image currentHPBar;
    public Image currentExpBar;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UPHP()
    {
        var player = PlayerStatus.instance;

        int maxHP = player.MaxHP;
        int currentHP = player.CurrentHP;

        maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString();

        currentHPBar.rectTransform.sizeDelta = new Vector2((float)currentHP / (float)maxHP * 230f, currentHPBar.rectTransform.sizeDelta.y);
    }

    public void SetHPBar(int _maxHP, int _currentHP)
    {
        int maxHP = _maxHP;
        int currentHP = _currentHP;

        maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString();

        currentHPBar.rectTransform.sizeDelta = new Vector2((float)currentHP/ (float)maxHP * 230f, currentHPBar.rectTransform.sizeDelta.y);
    }

    public void SetExpBar(int _level, int _maxEXP, int _currentEXP)
    {
        int maxEXP = _maxEXP;
        int currentEXP = _currentEXP;

        levelText.text = _level.ToString();
        maxEXPText.text = maxEXP.ToString();
        currentEXPText.text = currentEXP.ToString();

        currentExpBar.rectTransform.sizeDelta = new Vector2((float)currentEXP / (float)maxEXP * 230f, currentExpBar.rectTransform.sizeDelta.y);
    }
}
