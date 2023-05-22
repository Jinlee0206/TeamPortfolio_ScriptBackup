using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour 
{
    public SkillData mySkillData;


    public Image myIcon = null;

    float myCooldown;
    // Start is called before the first frame update
    void Start()
    {
        myCooldown = mySkillData.CoolTime;
        //GetComponent<Image>().sprite = mySkillData.Image;
        //myIcon.sprite = mySkillData.Image;
        myIcon.fillAmount = 1.0f;
        GetComponent<Image>().sprite = mySkillData.Image;
        myIcon.sprite = mySkillData.Image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCooldown(float myCooldown)
    {
        if (Mathf.Approximately(myIcon.fillAmount, 1.0f))
        {
            myIcon.fillAmount = 0.0f;
            StopAllCoroutines();
            StartCoroutine(Cooldowns(myCooldown));
        }
    }

    IEnumerator Cooldowns(float myCooldown)
    {
        while (myIcon.fillAmount < 1.0f)
        {
            myIcon.fillAmount += Time.deltaTime / myCooldown;
            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //StartCooldown(myCooldown);
    }
}
