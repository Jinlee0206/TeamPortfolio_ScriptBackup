using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
    
    public static SkillManager instance;                 // 자기 자신을 담을 static 변수
    public static SkillManager Instance => instance;     // 그 변수를 리턴할 static 프로퍼티 Instance

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SkillManager>(); // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬이 변경되더라도 자기 자신(싱글톤)을 파괴하지 않고 유지하도록 설정
        }
        else // 이미 유지되고 있는 싱글톤이 있다면
        {
            if (Instance != this) Destroy(this.gameObject); // 씬에 싱글톤 오브젝트가 된 다른 GameManager Object가 있다면 자신을 파괴
        }
    }

    public Dictionary<PlayerSkillName,bool> playerSkillCooldown = new Dictionary<PlayerSkillName, bool>(); //스킬이름이랑 bool 값 연결해서 true면 실행 X, false면 실행되지 않도록 
    [SerializeField] List<PlayerSkillName> key=new List<PlayerSkillName> ();
    private void Start() 
    {
       for(int i = 0; i < System.Enum.GetValues(typeof(PlayerSkillName)).Length; i++)
        {
            playerSkillCooldown.Add((PlayerSkillName)i, false);
            key.Add((PlayerSkillName)i);
        }
    }

    public void RegisterSkill(PlayerSkillName name,Transform Point)
    {
        if (!playerSkillCooldown[name])
        {
            playerSkillCooldown[name] = true;
            GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
            skill.GetComponent<ISkill>()?.Use();
            UIManager.instance.skillList.FindSlots(skill.GetComponent<ISkill>().skillData).
                StartCooldown(skill.GetComponent<ISkill>().skillData.CoolTime);
            StartCoroutine(CoolDown(name,skill.GetComponent<ISkill>().skillData.CoolTime));
        }
    }

    public void RegisterSkill(PlayerSkillName name, Vector3 Point,Quaternion quaternion)
    {
        if (!playerSkillCooldown[name])
        {
            playerSkillCooldown[name] = true;
            GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point, quaternion);
            skill.GetComponent<ISkill>()?.Use();
            UIManager.instance.skillList.FindSlots(skill.GetComponent<ISkill>().skillData).
                StartCooldown(skill.GetComponent<ISkill>().skillData.CoolTime);
            StartCoroutine(CoolDown(name, skill.GetComponent<ISkill>().skillData.CoolTime));
        }
    }

    public void RegisterSkill(MonsterSkillName name, Transform Point)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, Quaternion.identity);
        skill.GetComponent<ISkill>()?.Use();
    }

    public void RegisterSkill(MonsterSkillName name, Transform Point, Quaternion quaternion)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point.position, quaternion);
        skill.GetComponent<ISkill>()?.Use();
    }

    public void RegisterSkill(MonsterSkillName name, Vector3 Point, Quaternion quaternion = default)
    {
        GameObject skill = ObjectPoolingManager.instance.GetObject((name).ToString(), Point, quaternion);
        skill.GetComponent<ISkill>()?.Use();
    }

    IEnumerator CoolDown(PlayerSkillName name,float coolTime) //쿨다운 코루틴 쿨다운이 다 되면 false로 바꾸기.
    {
        float playTime = 0.0f;
        while (coolTime>playTime)
        {
            playTime += Time.deltaTime;
            yield return null;
        }
        playerSkillCooldown[name] = false;
        playTime = 0.0f;
    }
}

public enum PlayerSkillName
{
    EnergyBall,EnergyTornado
}

public enum MonsterSkillName
{
    DevilEye, MagicCircleImage, SpitFire
}