using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEye : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }

    public void Awake()
    {
        
    }

    public void Use()
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
