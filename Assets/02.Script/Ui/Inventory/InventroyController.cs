using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyController : MonoBehaviour
{
    [SerializeField]
    private InventoryButton invenButton;
    [SerializeField]
    private GameObject invenContent;

    private void Start()
    {
        // InvenData.instance.Initialized(invenButton, invenButton.Content);
    }
}
