using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    [SerializeField]
    private WeaponItem weaponItem;

    private void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickedButton);
    }

    public void OnClickedButton()
    {
        UIAndSceneManager.instance.ChangeEquipment(weaponItem);
    }
}
