using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortKeyManager : MonoBehaviour
{
    public static ShortKeyManager instance;

    [SerializeField]
    private List<ShortKeyItem> shortKeys = new List<ShortKeyItem>();

    private int shortkeyCnt;
    // Start is called before the first frame update
    void Awake()
    {   
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        shortkeyCnt = this.transform.childCount;
        for(int i = 0; i< shortkeyCnt; i++)
        {
            shortKeys.Add(this.transform.GetChild(i).GetComponent<ShortKeyItem>());
            shortKeys[i].shortkeyIndex = i;
        }
    }

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
}
