using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUIManager : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI currentHPText;
    public TextMeshProUGUI maxHPText;
    public TextMeshProUGUI sheildText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI criticalDamageText;
    public TextMeshProUGUI criticalPerText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI cooltimeText;
    public TextMeshProUGUI currentExp;
    public TextMeshProUGUI expSlice;
    public TextMeshProUGUI exp;

    public TextMeshProUGUI healthPointText;
    public TextMeshProUGUI dexPointText;
    public TextMeshProUGUI strPointText;
    public TextMeshProUGUI lukPointText;
    public TextMeshProUGUI bonusPointText;


    public GameObject healthUPBtn;
    public GameObject strUPBtn;
    public GameObject dexUPBtn;
    public GameObject lukUPBtn;

    public void SetStatusUIText()
    {
        var gameManager = Manager.Instance.Game;
        var player = PlayerInteractionStatus.instance;

        currentLevelText.text = gameManager.Level.ToString();
        currentHPText.text = player.CurrentHP.ToString();
        maxHPText.text = player.MaxHP.ToString();
        sheildText.text = player.Sheild.ToString();
        damageText.text = player.PlayerDamage.ToString();
        criticalDamageText.text = player.CriticalDamage.ToString();
        criticalPerText.text = player.CriticalPer.ToString();
        speedText.text = player.PlayerSpeed.ToString();
        cooltimeText.text = player.SkillCoolTime.ToString();

        currentExp.text = gameManager.CurrentExp.ToString();
        exp.text = gameManager.Exp.ToString();

        healthPointText.text = gameManager.Health.ToString();
        dexPointText.text = gameManager.Dex.ToString();
        strPointText.text = gameManager.Str.ToString();
        lukPointText.text = gameManager.Luk.ToString();
        bonusPointText.text = gameManager.BonusState.ToString();

        ExpWidthSet(gameManager.Exp);
    }

    public void ExpWidthSet(int cExp)
    {
        RectTransform currentExpRect = currentExp.rectTransform;
        RectTransform expSliceRect = expSlice.rectTransform;
        RectTransform expRect = exp.rectTransform;

        if (cExp <=99)
        {
            currentExpRect.sizeDelta = new Vector2(40f, 40f);
            expSliceRect.anchoredPosition = new Vector2(225f, 0f);
            expRect.anchoredPosition = new Vector2(230f, 0f);

            currentExp.fontSize = 26f;
            exp.fontSize = 26f;
        }
        else if(cExp <= 999)
        {
            currentExpRect.sizeDelta = new Vector2(60f, 40f);
            expSliceRect.anchoredPosition = new Vector2(245f, 0f);
            expRect.anchoredPosition = new Vector2(260f, 0f);

            currentExp.fontSize = 26f;
            exp.fontSize = 26f;
        }
        else if (cExp <= 9999)
        {
            currentExpRect.sizeDelta = new Vector2(75f, 40f);
            expSliceRect.anchoredPosition = new Vector2(260f, 0f);
            expRect.anchoredPosition = new Vector2(275f, 0f);

            currentExp.fontSize = 26f;
            exp.fontSize = 26f;
        }
        else if (cExp <= 99999)
        {
            currentExpRect.sizeDelta = new Vector2(90f, 40f);
            expSliceRect.anchoredPosition = new Vector2(275f, 0f);
            expRect.anchoredPosition = new Vector2(290f, 0f);

            currentExp.fontSize = 26f;
            exp.fontSize = 26f;
        }
        else if (cExp <= 999999)
        {
            currentExpRect.sizeDelta = new Vector2(90f, 40f);
            expSliceRect.anchoredPosition = new Vector2(275f, 0f);
            expRect.anchoredPosition = new Vector2(290f, 0f);

            currentExp.fontSize = 23f;
            exp.fontSize = 23f;
        }
        else if (cExp > 999999)
        {
            currentExpRect.sizeDelta = new Vector2(90f, 40f);
            expSliceRect.anchoredPosition = new Vector2(275f, 0f);
            expRect.anchoredPosition = new Vector2(290f, 0f);

            currentExp.fontSize = 21f;
            exp.fontSize = 21f;
        }
    }

    public void ViewAndHideStateButton()
    {
        if(Manager.Instance.Game.BonusState >= 1)
        {
            healthUPBtn.SetActive(true);
            strUPBtn.SetActive(true);
            dexUPBtn.SetActive(true);
            lukUPBtn.SetActive(true);
        }
        else
        {
            healthUPBtn.SetActive(false);
            strUPBtn.SetActive(false);
            dexUPBtn.SetActive(false);
            lukUPBtn.SetActive(false);
        }
    }

    public void HealthUP()
    {
        StatusUP("health");
    }

    public void STRUP()
    {
        StatusUP("str");
    }
     
    public void DEXUP()
    {
        StatusUP("dex");
    }

    public void LUKUP()
    {
        StatusUP("luk");
    }

    private void StatusUP(string status)
    {
        Manager.Instance.Game.PlayerStatusUp(status);
        ViewAndHideStateButton();
    }
}
