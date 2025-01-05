using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameNotionText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    // Text 설정
    public void SetNotionText(string msg)
    {
        text.text = msg;

        // 투명화 및 파괴 코루틴 실행
        StartCoroutine(DestroyedNotion());
    }

    // 일정 시간 이후 Notion Text 파괴
    private IEnumerator DestroyedNotion()
    {
        // 지속시간
        float duration = 3.0f;

        // 타이머
        float timer = 0.0f;
        
        Color originalColor = text.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // 투명화 값은 0(투명) ~ 1(불투명) 사이이다.
            float normalizeTime = timer / duration;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - normalizeTime);

            yield return null;
        }

        // 완전 투명화
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // 현제 오브젝트 파괴
        Destroy(this.gameObject);
    }
}
