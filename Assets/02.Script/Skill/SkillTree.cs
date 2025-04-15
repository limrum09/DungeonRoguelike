using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTreeCategory
{
    Public,
    OneHandSword,
    TwoHandSword,
    DoubleSword,
    Spear,    
    Magic
}

[CreateAssetMenu(menuName = "Skill/SkillTree", fileName = "_SkillTree")]
public class SkillTree : ScriptableObject
{
    [SerializeField]
    private SkillTreeCategory category;
    [SerializeField]
    private List<SkillNodeData> skillNodes;

    public SkillTreeCategory Category => category;
    public IReadOnlyList<SkillNodeData> SkillNodes => skillNodes;
}

[System.Serializable]
public class SkillNodeData
{
    public ActiveSkill skill;
}
