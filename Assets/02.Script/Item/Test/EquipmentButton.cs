using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
    // 2024 - 05, Equipment Button system change testing
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private SelectTest category;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickedButton);
    }

    public void OnClickedButton()
    {
        category.SelectItemButton(itemPrefab);        
    }
}
