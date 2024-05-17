using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    private GameObject player;

    private GameObject weaponR;
    private GameObject weaponL;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerComponent");
        weaponR = GameObject.FindGameObjectWithTag("TYPEWEAPONR");
        weaponL = GameObject.FindGameObjectWithTag("TYPEWEAPONL");
    }

    public void ChangeWeapon()
    {
        player.GetComponent<PlayerInteraction>().CheckActiveWeapon();
    }

    public void ActvieFalseWeapon()
    {
        foreach(Transform weapon in weaponL.transform)
        {
            if (weapon.gameObject.activeSelf)
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public void SelectSheild()
    {
        foreach (Transform weapon in weaponR.transform)
        {
            if (weapon.gameObject.activeSelf)
            {
                if(weapon.gameObject.tag != "OneHandSword")
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SelectLeftOneHandSword()
    {
        foreach (Transform weapon in weaponR.transform)
        {
            if (weapon.gameObject.activeSelf)
            {
                if (weapon.gameObject.tag != "OneHandSword")
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }
    }
}
