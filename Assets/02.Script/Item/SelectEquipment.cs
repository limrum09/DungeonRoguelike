using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEquipment : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private GameObject selectType;
    [SerializeField]
    public string selectString;
    private string lastHair;
    private string lastHead;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        selectType = GameObject.FindGameObjectWithTag(selectString);
        lastHair = null;
        lastHead = null;
    }

    private void TakeOffParts(string partTag)
    {
        foreach (Transform nonePart in selectType.transform)
        {
            if (nonePart.gameObject.tag == partTag)
            {
                nonePart.gameObject.SetActive(false);
            }

            if ((partTag == "HairPart" || partTag == "Hat" || partTag == "HeadArmor") && nonePart.name == lastHair)
            {
                nonePart.gameObject.SetActive(true);
            }
            else if(partTag == "Hair"){
                lastHair = null;
            }
        }
    }

    public void SelectPart(GameObject selectPart)
    {
        if(selectPart.name == "NonePart")
        {
            switch (selectPart.tag)
            {
                case "HairPart":
                    TakeOffParts("Hat");
                    TakeOffParts("HairPart");
                    TakeOffParts("HeadArmor");
                    break;
                case "Hair":
                    TakeOffParts("Hair");
                    break;
                case "Mask":
                    TakeOffParts("Mask");
                    break;
                case "Mustache":
                    TakeOffParts("Mustache");
                    break;
                case "OneHandSword":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Sheild");
                    TakeOffParts("Spear");
                    TakeOffParts("Wand");
                    break;
                case "TwoHandSword":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Spear");
                    TakeOffParts("Wand");
                    break;
                case "Spear":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Spear");
                    TakeOffParts("Wand");
                    break;
                case "Wand":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Spear");
                    TakeOffParts("Wand");
                    break;
            }
        }
        else
        {
            foreach (Transform currentPart in selectType.transform)
            {
                if (currentPart.name == selectPart.name)
                {
                    if (currentPart.gameObject.tag == selectPart.tag)
                    {
                        ChangePart(selectPart);
                        // 아이템 사용
                        currentPart.gameObject.SetActive(true);
                    }
                }
            }
        }        
    }

    // 현제 사용 중인 장비 SetActive(false)만들기
    private void ChangePart(GameObject part)
    {
        bool change = false;
        
        
        foreach (Transform currentPart in selectType.transform)
        {
            if (currentPart.tag == part.tag)
            {
                change = true;
                currentPart.gameObject.SetActive(false);
            }
        }
        if(change && part.tag == "Hair")
        {
            foreach(Transform hairpart in selectType.transform)
            {
                if(hairpart.tag == "Hair")
                {
                    lastHair = hairpart.name;
                }
                else if(hairpart.tag == "Hat")
                {
                    lastHair = hairpart.name;
                    hairpart.gameObject.SetActive(false);
                }
            }
        }
        // 바꾸려는 아이템이 모자일 경우 머리카락 안보이게 하기
        else if (change && part.tag == "Hat")
        {
            foreach (Transform hairPart in selectType.transform)
            {
                if (hairPart.tag == "Hair")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        lastHair = hairPart.name;
                        hairPart.gameObject.SetActive(false);
                    }
                }
                else if(hairPart.tag == "HairPart")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
               
                else if (hairPart.tag == "Head" && lastHead != null)
                {
                    if (hairPart.name == lastHead)
                    {
                        hairPart.gameObject.SetActive(true);
                    }
                }
            }
        }
        // 바꾸려는 아이템이 모자가 아닌 경우, 모자 안보이게 하기
        else if (change && part.tag == "HairPart" && (lastHair != null || lastHead != null))
        {
            foreach (Transform hairPart in selectType.transform)
            {
                if (hairPart.tag == "Hair")
                {
                    if (hairPart.name == lastHair)
                    {
                        hairPart.gameObject.SetActive(true);
                    }
                }
                else if(hairPart.tag == "Hat")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
                else if (hairPart.tag == "HeadArmor")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
                else if (hairPart.tag == "Head" && lastHead != null)
                {
                    if (hairPart.name == lastHead)
                    {
                        hairPart.gameObject.SetActive(true);
                    }
                }

            }
        }
        else if (change && part.tag == "HeadArmor")
        {
            foreach (Transform hairPart in selectType.transform)
            {
                if (hairPart.tag == "Head")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        lastHead = hairPart.name;
                        hairPart.gameObject.SetActive(false);
                    }
                }
                else if (hairPart.tag == "Hat")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
                else if (hairPart.tag == "Hair")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
                else if (hairPart.tag == "HairPart")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (change && part.tag == "Mask")
        {
            foreach (Transform hairPart in selectType.transform)
            {
                if (hairPart.tag == "Mustache")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (change && part.tag == "Mustache")
        {
            foreach (Transform hairPart in selectType.transform)
            {
                if (hairPart.tag == "Mask")
                {
                    if (hairPart.gameObject.activeSelf)
                    {
                        hairPart.gameObject.SetActive(false);
                    }
                }
            }
        }

        if(change &&(part.tag == "OneHandSword" || part.tag == "Sheild" || part.tag == "Wand" || part.tag == "TwoHandSword" || part.tag == "Spear" || part.tag == "Wand"))
        {
            switch (part.tag)
            {
                case "OneHandSword":
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Sheild");
                    TakeOffParts("Spear");
                    TakeOffParts("Wand");
                    break;
                case "TwoHandSword":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("Spear");
                    TakeOffParts("Wand");
                    break;
                case "Spear":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Wand");
                    break;
                case "Wand":
                    TakeOffParts("OneHandSword");
                    TakeOffParts("TwoHandSword");
                    TakeOffParts("Spear");                    
                    break;
                case "Sheild":
                    TakeOffParts("OneHandSword");
                    break;
            }
        }
    }
    
    private void LastChangeItem(GameObject item)
    {
        switch (item.tag)
        {
            case "Hat":
                break;
            case "Hair":
                break;
            case "HeadPart":
                break;
            case "HeadArmor":
                break;
            case "Head":
                break;
            case "Eye":
                break;
            case "Body":
                break;
            case "Clock":
                break;
            case "Mouse":
                break;
            case "Mask":
                break;
            case "Mustache":
                break;
        }
    }
}
