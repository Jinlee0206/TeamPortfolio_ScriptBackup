using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public SkillData skillData
    {
        get;set;
    }
    void Use()
    {

    }
}

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected Transform HitPoint;
}
