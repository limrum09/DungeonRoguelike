using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortKeyManager : MonoBehaviour
{
    [SerializeField]
    private List<ShortKeyItem> shortKeys = new List<ShortKeyItem>();

    private int shortkeyCnt;
    // Start is called before the first frame update
    public void ShortCutBoxStart()
    {
        shortkeyCnt = shortKeys.Count;
        for(int i = 0; i< shortkeyCnt; i++)
        {
            shortKeys.Add(this.transform.GetChild(i).GetComponent<ShortKeyItem>());
            shortKeys[i].SetIndex(i, $"ShortKey{i+1}");
        }
    }

    // 단축키의 값이 바뀌었으면, 바뀐 단축키의 key값을 넘겨줘서 해당 key의 위치에 있는 값을 다시 받아냄
    public void ChangeShortKey(string keyString)
    {
        foreach(var child in shortKeys)
        {
            if(child.InputShortKey == keyString)
            {
                child.SetShoryKey(keyString);
            }
        }
    }

    // 다른 아이템이 단축기에 있다면, 단축키에서 해당 아이템을 제거
    public void InputItemInShortkey(int index)
    {
        for(int i = 0; i< shortkeyCnt; i++)
        {
            if(i != index)
            {
                if (shortKeys[i].GetItem() == shortKeys[index].GetItem())
                {
                    shortKeys[i].HideItem();
                }
            }
            
        }
    }

    public void InputSkillInShortkey(int index)
    {
        for (int i = 0; i < shortkeyCnt; i++)
        {
            if (i != index)
            {
                if (shortKeys[i].GetSkill() == shortKeys[index].GetSkill())
                {
                    shortKeys[i].HideSkill();
                }
            }

        }
    }
}
