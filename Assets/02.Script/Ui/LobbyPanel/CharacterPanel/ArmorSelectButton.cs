using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSelectButton : MonoBehaviour
{
    [SerializeField]
    private ArmorItem armorItem;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickedButton);
    }

    public void OnClickedButton()
    {
        Manager.Instance.UIAndScene.ChangeEquipment(armorItem);
    }
}
