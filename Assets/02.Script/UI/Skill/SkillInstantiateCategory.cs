using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInstantiateCategory : MonoBehaviour
{
    [SerializeField]
    private SkillTreeCategory category;

    public SkillTreeCategory Category => category;
}
