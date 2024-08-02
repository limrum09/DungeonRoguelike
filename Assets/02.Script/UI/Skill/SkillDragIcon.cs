using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDragIcon : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private Image iconImage;

    private ActiveSkill skill;
    private bool isSkillDrag;

    public void SetActiveSkill(ActiveSkill getSkill)
    {
        skill = getSkill;
        iconImage.sprite = skill.SkillIcon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var selectUI = eventData.pointerEnter;

        if (selectUI != null && selectUI.GetComponent<UISkillImage>() != null)
        {
            isSkillDrag = true;

            ActiveSkill skill = selectUI.GetComponent<UISkillImage>().Skill;
            if (!skill.Conditions.All(x => x.IsSkillPass()))
            {                
                Debug.Log("조건 불만족");
                return;
            }

            SetActiveSkill(skill);

            iconImage.transform.position = Input.mousePosition;

            iconImage.gameObject.SetActive(true);
        }
    }



    public void OnDrag(PointerEventData eventData)
    {
        if (isSkillDrag)
        {
            iconImage.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSkillDrag)
        {
            isSkillDrag = false;
            iconImage.gameObject.SetActive(false);

            var endUI = eventData.pointerEnter;

            if(endUI != null && endUI.GetComponent<ShortKeyItem>())
            {
                ShortKeyItem key = endUI.GetComponent<ShortKeyItem>();

                key.RegisterInput(skill);
            }
        }
    }
}
