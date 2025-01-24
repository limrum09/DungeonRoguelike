using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCoroutine : MonoBehaviour
{
    public List<string> buffStrings = new List<string>();

    public void ListInitialized()
    {
        buffStrings.Clear();
    }

    public void PlayerBuffPotion(InvenItem item)
    {
        if (item == null || string.IsNullOrEmpty(item.ItemName))
        {
            Debug.LogError("아이템 또는, 아이템의 이름이 없습니다.");
            return;
        }

        string buffName = item.ItemName;

        if (buffStrings.Contains(buffName))
        {
            return;
        }
        
        buffStrings.Add(buffName);
        Manager.Instance.UIAndScene.BuffUI.PlayerGetBuff(item);

        StartCoroutine(UsingBuffItem(item));
    }

    private void GetPlayerBuffValue(int damage, float speed)
    {
        var gameManager = Manager.Instance.Game;

        gameManager.GetBuffDamage(damage);
        gameManager.GetBuffSpeed(speed);
    }

    private IEnumerator UsingBuffItem(InvenItem potion)
    {
        InvenItem newItem = potion;
        var player = PlayerInteractionStatus.instance;
        var notion = Manager.Instance.UIAndScene.Notion;

        string notionString;

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

            Debug.Log("힐 속도 : " + healSpeedTime);

            if (healSpeedTime == 0.0f)
            {
                Debug.Log("반복 시간 0초");
                isSustain = false;
                player.HealCurrentHP(hpHeal);
            }                
        }

        GetPlayerBuffValue(increaseDamage, increaseSpeed);

        notionString = $"{newItem.ItemName}을 사용했습니다.";
        notion.SetNotionText(notionString);

        while(buffTime >= 0)
        {
            buffTime -= Time.deltaTime;
            healTimer += Time.deltaTime;

            if(healTimer >= healSpeedTime && isSustain)
            {
                healTimer = 0.0f;
                player.HealCurrentHP(hpHeal);
                Debug.Log("체력 회복");
            }

            yield return null;
        }

        GetPlayerBuffValue(-increaseDamage, -increaseSpeed);

        notionString = $"{newItem.ItemName}의 효과가 사라졌습니다.";
        notion.SetNotionText(notionString);

        // 버프 제거
        if (!buffStrings.Remove(newItem.ItemName))
        {
            Debug.LogError($"buffStrings에서 {newItem.ItemName} 제거 실패. 현재 리스트 값:");
            foreach (var buff in buffStrings)
            {
                Debug.Log($"buffStrings: {buff}");
            }
        }
        Manager.Instance.UIAndScene.BuffUI.PlayerRemoveBuff(newItem);
    }

    private void OnApplicationQuit()
    {
        buffStrings.Clear();
    }
}
