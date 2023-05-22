using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySkillList : MonoBehaviour
{
    public List<SkillSlot> slots = new List<SkillSlot>();

    // Start is called before the first frame update
    void Start()
    {
        //Gamemanager.instance.myPlyaer.GetComponentInChildren<PlayerAnimEvent>().
        //  QSkillFunc.AddListener(() => slots[0].StartCooldown(slots[0].mySkillData.CoolTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SkillSlot FindSlots(SkillData myData)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].mySkillData == myData) return slots[i];
        }
        return null;
    }

}
