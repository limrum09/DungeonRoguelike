using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUIController : MonoBehaviour
{
    [SerializeField]
    private BuffUI buffUIPrefab;
    [SerializeField]
    private BuffInfoPanel buffInfoPanel;

    private RectTransform infoPanelRect;
    [SerializeField]
    private List<BuffUI> buffs = new List<BuffUI>();

    public void BuffUIControllerStart()
    {
        infoPanelRect = buffInfoPanel.GetComponent<RectTransform>();
        buffInfoPanel.gameObject.SetActive(false);
        
        // 초기화
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            // Icon 제거
            Destroy(buffs[i].gameObject);
            buffs.Remove(buffs[i]);
        }
    }

    public void PlayerGetBuff(InvenItem item)
    {
        BuffUI newBuff = Instantiate(buffUIPrefab, this.transform);
        newBuff.SetBuffIcon(item);
        buffs.Add(newBuff);
    }

    public void PlayerRemoveBuff(InvenItem item)
    {
        // 리스트를 수정하면서 순차적으로 루프를 도리면 인덱스값이 어긋날 수 있기에, 역순으로 루프를 돌린다.
        for(int i = buffs.Count - 1;i >= 0; i--)
        {
            if (buffs[i].Item.Equals(item))
            {
                // Icon 제거
                Destroy(buffs[i].gameObject);
                buffs.Remove(buffs[i]);
            }
        }

        HideBuffInfoPanel();
    }

    public void ViewBuffInfoPanel(InvenItem item, float posX, float posY)
    {
        if (item == null)
            return;

        if (infoPanelRect == null)
            infoPanelRect = buffInfoPanel.GetComponent<RectTransform>();

        Vector2 tooltipPos = new Vector2(posX + 95f, posY);
        buffInfoPanel.transform.position = tooltipPos;

        buffInfoPanel.gameObject.SetActive(true);
        buffInfoPanel.ViewBuffInfo(item.ItemName, item.HPHealValue, item.HealSpeedTime, item.InCreaseDamageValue,item.InCreaseSpeedValue);
    }

    public void HideBuffInfoPanel()
    {
        buffInfoPanel.gameObject.SetActive(false);
    }
}
