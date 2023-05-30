using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISkill
{
    public SkillData skillData
    {
        get;set;
    }
    void Use(UnityAction e = null);
}

public abstract class Skill : MonoBehaviour
{
    [SerializeField]
    protected Transform HitPoint;
}
