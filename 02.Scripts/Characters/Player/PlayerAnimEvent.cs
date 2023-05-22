using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;
    public UnityEvent AttackEnterFunc;
    public UnityEvent AttackExitFunc;
    public UnityEvent SkillFunc;
    public UnityEvent SkillFunc2;
    // Start is called before the first frame update
    public void OnAttackEnter() //Attack이 시작될 때 실행.
    {
        AttackEnterFunc?.Invoke();
    }
    public void OnAttack() //타격 판정
    {
        AttackFunc?.Invoke();
    }

    public void OnAttackExit() //Attack이 끝날 때 실행.
    {
        AttackExitFunc?.Invoke();
    }

    public void SkillOn()
    {
        SkillFunc?.Invoke();
    }

   public void SkillOn2()
    {
        SkillFunc2?.Invoke();
    }
}
