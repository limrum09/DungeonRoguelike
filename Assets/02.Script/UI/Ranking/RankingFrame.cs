using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingFrame : MonoBehaviour
{
    [Header("Data")]
    [SerializeField]
    private int rank;
    [SerializeField]
    private int level;
    [SerializeField]
    private string nickname;


    [Header("Objects")]
    [SerializeField]
    private TextMeshProUGUI rankingText;
    [SerializeField]
    private Image rankerImage;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI nicknameText;
    [SerializeField]
    private Image icon;

    public int Rank
    {
        set
        {
            rank = value;
            if (value >= 0)
            {
                rankingText.text = rank.ToString();
            }
            else
            {
                rankingText.text = "Not";
            }
            
        }
        get 
        {
            return rank;
        }
    }

    public int Level
    {
        set
        {
            level = value;
            if (value >= 0)
            {
                levelText.text = $"Lv. {level}";
            }
            else
            {
                levelText.text = "데이터가 존재하지 않습니다.";
            }            
        }
        get => level;
    }

    public string Nickname
    {
        set
        {
            nickname = value;
            nicknameText.text = nickname;
        }
        get => nickname;
    }

    public void SetRanking()
    {
        rankingText.text = "None";
        rankerImage.sprite = null;
        levelText.text = "None";
        nicknameText.text = "None";
        icon.gameObject.SetActive(false);
    }
}
