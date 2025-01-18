using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCoroutine : MonoBehaviour
{
    public List<string> buffStrings = new List<string>();

    public void PlayerBuffPotion(InvenItem item)
    {
        if (item == null || string.IsNullOrEmpty(item.ItemName))
        {
            Debug.LogError("아이템 또는, 아이템의 이름이 없습니다.");
            return;
        }

        string buffName = item.ItemName;

        if (buffStrings.Contains(buffName))
            return;
        
        buffStrings.Add(buffName);

        StartCoroutine(UsingBuffItem(item));
    }

    private void GetPlayerBuffValue(int damage, float speed)
    {
        var gameManager = Manager.Instance.Game;

        gameManager.GetBuffDamage(damage);
        gameManager.GetBuffSpeed(speed);
    }

    public IEnumerator UsingBuffItem(InvenItem potion)
    {
        InvenItem newItem = potion;
        var player = PlayerInteractionStatus.instance;
        var notion = Manager.Instance.UIAndScene.Notion;

        string notionString;

        float timer = 0.0f;
        float healTimer = 0.0f;

        float buffTime = newItem.DurationTime;
        int hpHeal = newItem.HPHealValue;
        int mpHeal = newItem.MPHealValue;
        int increaseDamage = newItem.InCreaseDamageValue;
        float increaseSpeed = newItem.InCreaseSpeedValue;

        bool isSustain = newItem.IsSustain;
        float healSpeedTime = 0.0f;

        if (isSustain)
        {
            healSpeedTime = newItem.HealSpeedTime;

            if (healSpeedTime == 0.0f)
                player.HealCurrentHP(hpHeal);
        }

        GetPlayerBuffValue(increaseDamage, increaseSpeed);

        notionString = $"{newItem.ItemName}을 사용했습니다.";
        notion.SetNotionText(notionString);

        while(buffTime >= 0)
        {
            buffTime -= Time.deltaTime;
            healTimer += Time.deltaTime;

            if(healTimer >= healSpeedTime)
            {
                healTimer = 0.0f;
                player.HealCurrentHP(hpHeal);
            }

            yield return null;
        }

        GetPlayerBuffValue(-increaseDamage, -increaseSpeed);

        notionString = $"{newItem.ItemName}의 효과가 사라졌습니다.";
        notion.SetNotionText(notionString);

        buffStrings.Remove(newItem.name);
    }
}
