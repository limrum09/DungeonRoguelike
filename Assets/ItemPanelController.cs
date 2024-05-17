using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public GameObject itemPanel;
    private bool inUI;
    Ray ray;
    public void OnPointerEnter(PointerEventData eventData)
    {
        inUI = true;
        ItemInfoPanelMove();
        Debug.Log("Mouse Enter UI");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inUI = false;
        Debug.Log("Mouse Exit UI");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        inUI = true;
        //ItemInfoPanelMove();
        Vector2 eventPos = new Vector2(eventData.position.x + 240f, eventData.position.y - 100f);
        itemPanel.transform.position = eventPos;
        Debug.Log("Mouse Move UI");
    }

    // Start is called before the first frame update
    void Start()
    {
        inUI = false;   
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 위치 좌표 변환
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (inUI)
        {
            
        }
    }

    public void ItemInfoPanelMove()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x + 220f, Input.mousePosition.y);
        itemPanel.transform.position = mousePosition;
    }
}
