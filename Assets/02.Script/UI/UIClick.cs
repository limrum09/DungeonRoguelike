using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var selectUI = eventData.pointerEnter;

        if (selectUI.transform.gameObject.CompareTag("GameUI"))
        {
            selectUI.transform.parent.SetAsLastSibling();
        }
    }
}
