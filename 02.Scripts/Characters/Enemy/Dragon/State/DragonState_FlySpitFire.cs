using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_FlySpitFire : State
{
    Dragon dragon;

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
        //Vector3 dir = (dragon.myTarget.transform.position - dragon.transform.position).normalized;

        //SkillManager.instance.RegisterSkill(MonsterSkillName.MagicCircleImage, dragon.transform, Quaternion.Euler(new Vector3(40.0f, 0f, 0f)));
        SkillManager.instance.RegisterSkill(MonsterSkillName.MagicCircleImage, dragon.spitFirePos, dragon.spitFirePos.localRotation);
        //yield return null;
        yield return new WaitForSeconds(3.0f);
        var wfs = new WaitForSeconds(dragon.spitFireDelay);

        ////Vector3 fireStartPos = new Vector3(0, -10.0f, 16f) + magicCircle;
        //Vector3 fireStartPos = new Vector3(0, -10.0f, 16f) + dragon.spitFirePos.localPosition;

        while (dragon.spitFireCnt < 5)
        {
            dragon.spitFireCnt++;
            dragon.myAnim.SetTrigger("FlySpitFire");
            Debug.Log($"{dragon.spitFireCnt}");

            SkillManager.instance.RegisterSkill(MonsterSkillName.SpitFire, dragon.spitFirePos, Quaternion.Euler(new Vector3(135.0f, 0f, 0f)));

            yield return wfs;
        }
        
        stateMachine.ChangeState(dragon.m_states[Dragon.eState.Landing]);


    }

}
