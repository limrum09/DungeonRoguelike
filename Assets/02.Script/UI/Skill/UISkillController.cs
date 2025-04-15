using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillController : MonoBehaviour
{
    [Header("Skill Trees")]
    [SerializeField]
    private SkillTree publicSkillTree;
    [SerializeField]
    private List<SkillTree> weaponSkillTrees;
    [SerializeField]
    private List<SkillInstantiateCategory> categorys;

    [Header("Prefab")]
    [SerializeField]
    private UISkillImage skillPrefab;

    [Header("Rects")]
    [SerializeField]
    private RectTransform scrollViewContentRect;
    [SerializeField]
    private RectTransform publicAreaRect;
    [SerializeField]
    private RectTransform weaponAreaRect;

    [Header("Skill Info")]
    [SerializeField]
    private UISkillInfoController skillInfo;

    public void SkillUIStart()
    {
        SkillUIInitialized();
        Manager.Instance.Game.PlayerCurrentStatus.OnSkillPointChanged += skillInfo.RefreshSkillPoint;

        // Weapon Skill 오브젝트의 크기를 재조절하기 위해 필요
        int biggestCnt = 0;
        // 스킬 추가위치
        Transform categoryTf = null;

        foreach(SkillTree skillTree in weaponSkillTrees)
        {
            categoryTf = categorys.Find(category => category.Category == skillTree.Category).transform;

            int skillCount = skillTree.SkillNodes.Count;

            if (biggestCnt < skillCount)
                biggestCnt = skillCount;

            for(int i = 0; i < skillCount; i++)
            {
                Instantiate(skillPrefab, categoryTf).SkillImageStart(skillTree.SkillNodes[i].skill);
            }

            RectTransform rect = categoryTf.gameObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, skillCount * 130f);
        }

        categoryTf = categorys.Find(category => category.Category == publicSkillTree.Category).transform;

        int publicCnt = publicSkillTree.SkillNodes.Count / 5 + 1;
        for (int i = 0; i < publicSkillTree.SkillNodes.Count; i++)
        {
            Instantiate(skillPrefab, categoryTf).SkillImageStart(publicSkillTree.SkillNodes[i].skill);
        }

        // UI 크기 재조정
        publicAreaRect.sizeDelta = new Vector2(publicAreaRect.sizeDelta.x, 150f * publicCnt);
        weaponAreaRect.sizeDelta = new Vector2(weaponAreaRect.sizeDelta.x, 130f * biggestCnt);
        scrollViewContentRect.sizeDelta = new Vector2(scrollViewContentRect.sizeDelta.x, publicAreaRect.sizeDelta.y + weaponAreaRect.sizeDelta.y);
    }

    public void SkillLevelUp() => skillInfo.SkillLevelUp();

    public void SelectSkill(ActiveSkill skill) => skillInfo.SelectSkill(skill);

    private void SkillUIInitialized() => skillInfo.SkillInfoUIInitialized();

    private void OnDestroy()
    {
        Manager.Instance.Game.PlayerCurrentStatus.OnSkillPointChanged -= skillInfo.RefreshSkillPoint;
    }
}
