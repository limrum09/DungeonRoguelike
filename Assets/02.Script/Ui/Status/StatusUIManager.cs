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
        var gameManager = GameManager.instance;
        var player = PlayerStatus.instance;

        currentLevelText.text = gameManager.level.ToString();
        currentHPText.text = player.CurrentHP.ToString();
        maxHPText.text = player.MaxHP.ToString();
        sheildText.text = player.Sheild.ToString();
        damageText.text = player.PlayerDamage.ToString();
        criticalDamageText.text = player.CriticalDamage.ToString();
        criticalPerText.text = player.CriticalPer.ToString();
        speedText.text = player.PlayerSpeed.ToString();
        cooltimeText.text = player.SkillCoolTime.ToString();
        currentExp.text = player.CurrentExp.ToString();
        exp.text = player.Exp.ToString();

        healthPointText.text = gameManager.health.ToString();
        dexPointText.text = gameManager.dex.ToString();
        strPointText.text = gameManager.str.ToString();
        lukPointText.text = gameManager.luk.ToString();
        bonusPointText.text = gameManager.bonusState.ToString();

        ExpWidthSet(player.Exp);
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
        if(GameManager.instance.bonusState >= 1)
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
        GameManager.instance.PlayerStatusUp(status);
        ViewAndHideStateButton();
    }

/*    private void StatusUP(string status)
    {
        var gameManager = GameManager.instance;
        bool useState = false;

        if (gameManager.bonusState >= 1)
        {
            switch (status)
            {
                case "health":
                    gameManager.health++;
                    useState = true;
                    break;
                case "str":
                    gameManager.str++;
                    useState = true;
                    break;
                case "dex":
                    gameManager.dex++;
                    useState = true;
                    break;
                case "luk":
                    gameManager.luk++;
                    useState = true;
                    break;
                default:
                    useState = false;
                    break;
            }

            if (useState)
            {
                gameManager.bonusState--;
                gameManager.ChangePlayerStatus();
            }
            else
                Debug.Log("Can't use bonus status");
            ViewAndHideStateButton();
        }
        else
            ViewAndHideStateButton();
    }*/
}
