using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DevilEye : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }

    public void Awake()
    {
        
    }

    public void Use(UnityAction e = null)
    {
        StartCoroutine(Using());
    }

    IEnumerator Using()
    {
        var wfs = new WaitForSeconds(5.0f);
        yield return wfs;
        ObjectPoolingManager.instance.ReturnObject(gameObject);
        yield return null;
    }
}
