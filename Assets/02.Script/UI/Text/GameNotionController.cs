using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNotionController : MonoBehaviour
{
    [SerializeField]
    private GameNotionText notion;

    public void SetNotionText(string msg)
    {
        // 새로운 Notion을 자식으로 만들기
        GameNotionText newNotion = Instantiate(notion, this.transform);
        newNotion.transform.SetAsFirstSibling();

        // Text 넘겨주기
        newNotion.SetNotionText(msg);
    }
}
