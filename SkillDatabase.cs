using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SkillDatabase", menuName = "GameManager/Database/SkillDatabase")]
public class SkillDatabase : GameDatabase<ActiveSkill>
{
    
}