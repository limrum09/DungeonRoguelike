using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class EquipmentSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected RectTransform rectTf;
    protected Button btn;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClickedButton);
        rectTf = this.gameObject.GetComponent<RectTransform>();
    }

    protected abstract void OnClickedButton();

    public virtual void OnPointerEnter(PointerEventData eventData) { }

    public void OnPointerExit(PointerEventData eventData)
    {
        Manager.Instance.UIAndScene.EquipmentSelectUI.HideItemInfo();
    }
}
