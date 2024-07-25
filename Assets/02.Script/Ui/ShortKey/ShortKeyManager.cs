using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortKeyManager : MonoBehaviour
{
    [SerializeField]
    private List<ShortKeyItem> shortKeys = new List<ShortKeyItem>();

    private int shortkeyCnt;
    // Start is called before the first frame update
    void Start()
    {   
        shortkeyCnt = this.transform.childCount;
        for(int i = 0; i< shortkeyCnt; i++)
        {
            shortKeys.Add(this.transform.GetChild(i).GetComponent<ShortKeyItem>());
            shortKeys[i].SetIndex(i, $"ShortKey{i+1}");
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
