using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectEquipmentButton : MonoBehaviour
{
    [SerializeField]
    public GameObject partType;
    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        if(button != null)
        {
            button.onClick.AddListener(SelectPart);
        }
    }

    private GameObject FindChild(Transform parent, string childName)
    {
        foreach(Transform child in parent)
        {
            if(child.name == childName)
            {
                return child.gameObject;
            }

            GameObject foundChild = FindChild(child, childName);
            if(foundChild != null)
            {
                return foundChild;
            }
        }

        return null;
    }

    public void SelectPart()
    {
        partType.GetComponent<SelectEquipment>().SelectPart(this.gameObject);
        if (partType.GetComponent<SelectWeapon>())
        {
            partType.GetComponent<SelectWeapon>().ChangeWeapon();
        }        
    }
}
