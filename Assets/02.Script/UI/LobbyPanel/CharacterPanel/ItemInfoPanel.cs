using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject itemInfoPanel;

    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemUsingOneHandText;
    [SerializeField]
    private TextMeshProUGUI itemCategoryText;
    [SerializeField]
    private TextMeshProUGUI itemHealthText;
    [SerializeField]
    private TextMeshProUGUI itemBasicDamageText;
    [SerializeField]
    private TextMeshProUGUI itemCriticalDamageText;
    [SerializeField]
    private TextMeshProUGUI itemSheildText;
    [SerializeField]
    private TextMeshProUGUI itemCriticalPerText;
    [SerializeField]
    private TextMeshProUGUI itemSpeedText;
    [SerializeField]
    private TextMeshProUGUI itemCoolTimerText;
    [SerializeField]
    private TextMeshProUGUI itemConditionText;


    public void ResetInfoPanel()
    {
        itemImage.sprite = null;
        itemNameText.text = "초기화 됨";
        itemUsingOneHandText.text = null;
        itemCategoryText.text = null;

        itemHealthText.text = null;
        itemBasicDamageText.text = null;
        itemCriticalDamageText.text = null;
        itemSheildText.text = null;
        itemCriticalPerText.text = null;
        itemSpeedText.text = null;
        itemCoolTimerText.text = null;


        itemConditionText.text = "";
    }

    public void SetItemInfo(WeaponItem item)
    {
        itemImage.sprite = item.ItemSprite;
        itemNameText.text = item.ItemName;
        itemUsingOneHandText.text = CheckItemUsingOneHand(item.UseOndeHand);
        itemCategoryText.text = "장비분류 : " + CheckWeaponItemCategory(item.WeaponValue);

        itemHealthText.text = $"+ {item.ItemHP}";
        itemBasicDamageText.text = $"+ {item.ItemDamage}";
        itemCriticalDamageText.text = $"+ {item.ItemCriticalDamage}";
        itemSheildText.text = $"+ {item.ItemSheild}";
        itemCriticalPerText.text = $"+ {item.ItemCriticalPer}";
        itemSpeedText.text = $"+ {item.ItemSpeed}";
        itemCoolTimerText.text = $"+ {item.ItemCoolTime}";

        if (item.Condition != null && !item.Condition.IsAchievementPass)
        {
            itemConditionText.text = ItemConditionString(item.Condition.TargetName, item.Condition);
        }            
        else
            itemConditionText.text = "";
    }

    public void SetItemInfo(ArmorItem item)
    {
        itemImage.sprite = item.ItemSprite;
        itemNameText.text = item.ItemName;
        itemUsingOneHandText.text = "";
        itemCategoryText.text = "장비분류 : " +  CheckArmorItemCategory(item.SubCategory);

        itemHealthText.text = $"+ {item.ItemHP}";
        itemBasicDamageText.text = $"+ {item.ItemDamage}";
        itemCriticalDamageText.text = $"+ {item.ItemCriticalDamage}";
        itemSheildText.text = $"+ {item.ItemSheild}";
        itemCriticalPerText.text = $"+ {item.ItemCriticalPer}";
        itemSpeedText.text = $"+ {item.ItemSpeed}";
        itemCoolTimerText.text = $"+ {item.ItemCoolTime}";


        if (!item.Condition.IsAchievementPass)
        {
            itemConditionText.text = ItemConditionString(item.Condition.TargetName, item.Condition);
        }
        else
            itemConditionText.text = "";
    }

    private string ItemConditionString(string achievementName, IsAchievementComplete condition)
    {
        string conditionText = "";

        if (condition.IsAchievementPass)
            conditionText += $"<color=#14C800> 업적 : [{achievementName}] 달성</color>";
        else
            conditionText += $"<color=#FA0000> 업적 : [{achievementName}] 달성 필요</color>";

        return conditionText;
    }

    private string CheckItemUsingOneHand(bool useHand)
    {
        string result;

        if (useHand)
            result = "한손무기";
        else
            result = "두손무기";

        return result;
    }

    private string CheckWeaponItemCategory(int value)
    {
        string result;

        switch (value)
        {
            case 1:
                result = "한손검";
                break;
            case 2:
                result = "방패";
                break;
            case 3:
                result = "양손검";
                break;
            case 4:
                result = "창";
                break;
            case 5:
                result = "완드";
                break;
            default:
                result = "미정";
                break;
        }

        return result;
    }

    private string CheckArmorItemCategory(string value)
    {
        string result;

        switch (value)
        {
            case "hat":
                result = "모자";
                break;
            case "hair":
                result = "머리카락";
                break;
            case "eye":
                result = "눈장식";
                    break;
            case "head":
                result = "머리";
                break;
            case "Mouth":
                result = "입술";
                break;
            case "Mustache":
                result = "콧수염";
                break;
            case "NinjaMask":
                result = "마스크";
                break;
            default:
                result = "미정";
                break;
        }

        return result;
    }
}
