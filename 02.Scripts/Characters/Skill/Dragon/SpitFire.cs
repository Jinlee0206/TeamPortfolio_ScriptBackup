using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpitFire : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }
    [SerializeField]
    SpitFireTrigger trigger;
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

    public void Update()
    {
        //if (trigger.isHit)
        //{
        //    trigger.hitObject.GetComponent<IBattle>()?.OnDamage(5);
        //}
    }
}
