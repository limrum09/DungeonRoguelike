using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillImageController : MonoBehaviour
{
    [SerializeField]
    private List<UISkillImage> skillImages;

    public void UISkillImageStart()
    {
        foreach(UISkillImage skillImage in skillImages)
        {
            if(skillImage != null)
                skillImage.SkillImageStart();
        }
    }
}
