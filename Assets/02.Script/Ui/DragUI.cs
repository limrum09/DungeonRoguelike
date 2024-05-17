using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    GameObject ui;
    bool isDrag;

    Vector2 startinPoint;
    Vector2 clickPont;
    Vector2 offset;
    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            offset = eventData.position - clickPont;
            ui.transform.position = startinPoint + offset;
        }        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var selectUI = eventData.pointerEnter;

        if (selectUI != null && selectUI.CompareTag("DragUI"))
        {
            ui = selectUI.transform.parent.gameObject;
            int index = ui.transform.childCount - 1;
            ui.transform.SetSiblingIndex(index);

            startinPoint = ui.transform.position;
            clickPont = eventData.position;

            isDrag = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ui = null;
        isDrag = false;
    }
}
