using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [System.Serializable]
    private class OnUITextClickEvent : UnityEvent {}
    [SerializeField]
    private OnUITextClickEvent onUITextClickEvent;

    [SerializeField]
    private TextMeshProUGUI text;

    public void OnPointerClick(PointerEventData eventData)
    {
        text.fontSize = 36;
        text.fontStyle = FontStyles.Normal;
        onUITextClickEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = 42;
        text.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = 36;
        text.fontStyle = FontStyles.Normal;
    }
}
