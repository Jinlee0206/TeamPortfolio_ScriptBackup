using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DragonState_FlySpitFire : State
{
    Dragon dragon;
    Vector3 spitFireRotation;
    public DragonState_FlySpitFire(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StartCoroutine(SpitFire());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    IEnumerator SpitFire()
    {
        SkillManager.instance.RegisterSkill(MonsterSkillName.MagicCircleImage, dragon.spitFirePos, dragon.spitFirePos.rotation);
        yield return new WaitForSeconds(3.0f);
        spitFireRotation = new Vector3(0f, dragon.transform.localRotation.y, 0f);
        Debug.Log($"{spitFireRotation}");
        var wfs = new WaitForSeconds(dragon.spitFireDelay);

        while (dragon.spitFireCnt < 25)
        {
            dragon.spitFireCnt++;
            dragon.myAnim.SetTrigger("FlySpitFire");    
            Debug.Log($"{dragon.spitFireCnt}");

            SkillManager.instance.RegisterSkill(MonsterSkillName.SpitFire, dragon.spitFirePos.position+new Vector3(0,1,0)*10.0f,dragon.transform.localRotation);

            yield return wfs;
        }
        stateMachine.ChangeState(dragon.m_states[Dragon.eState.Landing]);
    }
}
