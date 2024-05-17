using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    public  GameObject HandR;
    [SerializeField]
    public GameObject HandL;
    [SerializeField]
    private GameObject weaponR;
    [SerializeField]
    private GameObject weaponL;
    [SerializeField]
    private Animator animator;

    
    public List<RuntimeAnimatorController> animatorController;

    // Start is called before the first frame update
    void Start()
    {
        CheckActiveWeapon();
    }

    // 사용 중인 무기
    public void CheckActiveWeapon()
    {
        foreach(Transform child in HandR.transform)
        {
            if (child.gameObject.activeSelf)
            {
                weaponR = child.gameObject;
            }
        }

        foreach (Transform child in HandL.transform)
        {
            if (child.gameObject.activeSelf)
            {
                weaponL = child.gameObject;
            }
        }

        WeaponAnimation();
    }

    public void WeaponAnimation()
    {
        string selectAnimator = "";

        
        if ((weaponR.activeSelf == true && weaponR.tag == "TwoHandSword") && (weaponL == null || weaponL.activeSelf == false))
        {
            selectAnimator = "TwoHandSword";
        }
        else if ((weaponR.activeSelf == true && weaponR.tag == "OneHandSword") && (weaponL == null || weaponL.activeSelf == false))
        {
            selectAnimator = "SingleSword";
        }
        else if((weaponR.activeSelf == true && weaponR.tag == "Wand") && (weaponL == null || weaponL.activeSelf == false))
        {
            selectAnimator = "MagicWand";
        }
        else if ((weaponR.activeSelf == true && weaponR.tag == "Spear") && (weaponL == null || weaponL.activeSelf == false))
        {
            selectAnimator = "Spear";
        }
        else if ((weaponR.tag == "OneHandSword" && weaponL.tag == "OneHandSword") || ((weaponL != null && weaponL.tag == "OneHandSword")))
        {
            if(weaponR.activeSelf == false)
            {
                selectAnimator = "NoWeapon";
            }
            else
            {
                selectAnimator = "DoubleSword";
            }
        }
        else if(weaponL != null && weaponL.tag == "Sheild")
        {
            if (weaponR.activeSelf == false)
            {
                selectAnimator = "NoWeapon";
            }
            else
            {
                selectAnimator = "SwordAndSheild";
            }
        }
        else if ((weaponR == null || weaponR.activeSelf == false) && (weaponL == null || weaponL.activeSelf == false))
        {
            selectAnimator = "NoWeapon";
        }

        switch (selectAnimator)
        {
            case "SwordAndSheild":
                animator.runtimeAnimatorController = animatorController[0];
                break;
            case "TwoHandSword":
                animator.runtimeAnimatorController = animatorController[1];
                break;
            case "Spear":
                animator.runtimeAnimatorController = animatorController[2];
                break;
            case "SingleSword":
                animator.runtimeAnimatorController = animatorController[3];
                break;
            case "DoubleSword":
                animator.runtimeAnimatorController = animatorController[4];
                break;
            case "MagicWand":
                animator.runtimeAnimatorController = animatorController[5];
                break;
            case "NoWeapon" :
                animator.runtimeAnimatorController = animatorController[6];
                break;
            default:
                Debug.Log("SelectAnimator : " + selectAnimator);
                break;
        }
    }
}
