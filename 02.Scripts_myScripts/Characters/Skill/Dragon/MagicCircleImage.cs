using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicCircleImage : Skill, ISkill
{
    [field:SerializeField]
    public SkillData skillData { get; set; }

    float dist;
    Vector3 MagicCircleMaxScale;
    Vector3 MagicCircleMinScale;
    Vector3 SpitFireRotation;

    [SerializeField]
    Animator animator;
    public void Awake()
    {
        dist = skillData.Distance;
        MagicCircleMaxScale = new Vector3(50, 50, 50);
        MagicCircleMinScale = new Vector3(35, 35,35);
        SpitFireRotation = new Vector3(135.0f, 0f, 0f);
    }

    public void Use(UnityAction e = null)
    {
        StartCoroutine(Using());
    }

    IEnumerator Using()
    {

        ////float offset = 4.0f;
        //while (dist < 0.0f)
        //{
        //    float delta = Time.deltaTime;
        //    if (dist < delta)
        //    {
        //        delta = dist;
        //    }
        //    dist -= delta;
        //    //transform.Translate(transform.forward * delta, Space.World);
        //    transform.localScale= Vector3.Lerp(transform.localScale, new Vector3(90,90,90), Time.deltaTime);
        //    //Mathf.Lerp(10.0f, 30.0f, Time.deltaTime * offset);
        //    yield return null;
        //}
        animator.SetTrigger("Intro");
        //while (transform.localScale.x< MagicCircleMaxScale.x-0.1f)
        //{
       
        //    //transform.Translate(transform.forward * delta, Space.World);
        //    transform.localScale = Vector3.Lerp(transform.localScale, MagicCircleMaxScale, Time.deltaTime);
        //    //Mathf.Lerp(10.0f, 30.0f, Time.deltaTime * offset);
        //    yield return null;
        //}


        var wfs = new WaitForSeconds(18.0f);
        yield return wfs;
        //while (transform.localScale.x > MagicCircleMinScale.x + 0.1f)
        //{

        //    //transform.Translate(transform.forward * delta, Space.World);
        //    transform.localScale = Vector3.Lerp(MagicCircleMaxScale, MagicCircleMinScale, Time.deltaTime * 1.5f);
        //    //Mathf.Lerp(10.0f, 30.0f, Time.deltaTime * offset);
        //    yield return null;
        //}
        animator.SetTrigger("Outro");
        while (!animator.GetBool("OutroEnd"))
        {
            //Debug.Log($"{animator.GetBool("OutroEnd")}");
            yield return null;
        }
        ObjectPoolingManager.instance.ReturnObject(gameObject);

    }
}
