using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTornado : Skill,ISkill
{
    [field: SerializeField]
    public SkillData skillData
    {
        get; set;
    }
    [SerializeField]
    LayerMask enemyMask;
    public float dist;
    public void Awake()
    {
        dist = skillData.Distance;
    }
    public void Use()
    {
        StartCoroutine(Using());
    }

    IEnumerator Using()
    {
        while (dist > 0.0f)
        {
            float delta = skillData.Speed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(transform.forward * delta, Space.World);
            yield return null;
        }
        dist = skillData.Distance;
        ObjectPoolingManager.instance.ReturnObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            other.GetComponent<IBattle>()?.OnDamage(skillData.Value1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Test");
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            other.GetComponent<IBattle>()?.OnDamage(skillData.Value1);
        }
    }
}
