using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Shield
}

public class BuffSystem : MonoBehaviour
{ 

    Dictionary<BuffType, List<GameObject>> BuffList=new Dictionary<BuffType, List<GameObject>>();

    public void AddBuff(GameObject character, BuffType type)
    {
        BuffList.Add(type, new List<GameObject>());
        BuffList[type].Add(character);
    }

    public void DeleteBuff(GameObject character, BuffType type)
    {
        if (BuffList.ContainsKey(type))
        {
            if (BuffList[type].Contains(character))
                BuffList[type].Remove(character);
        }
    }

    public void DeleteAllBuff(GameObject character)
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            if (BuffList[(BuffType)i].Contains(character))
            {
                BuffList[(BuffType)i].Remove(character);
            }
        }
    }
    public bool ContainBuff(GameObject character,BuffType type)
    {
        if (BuffList.ContainsKey(type))
        {
            GameObject[] obj = BuffList[type].ToArray();
            for(int i = 0; i < obj.Length; i++)
            {
                if (System.Object.ReferenceEquals(character, obj[i]))
                    return true;
            }
        }
        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
