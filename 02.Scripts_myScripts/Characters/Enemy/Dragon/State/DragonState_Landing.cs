using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_Landing : State
{
    Dragon dragon;

    public DragonState_Landing(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
        dragon.myAnim.SetTrigger("Landing");
        dragon.StartCoroutine(Landing());
    }

    public override void Exit()
    {
        base.Exit();
        dragon.isFlying = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator Landing()
    {
        float landTime = 0f;

        Vector3 startPos = dragon.transform.position;
        Vector3 targetPos = dragon.orgPos;

        while (landTime < 1.0f)
        {
            if(landTime > 0.8f && dragon.myAnim.GetBool("isFlying")) dragon.myAnim.SetBool("isFlying", false);
            landTime += Time.deltaTime / dragon.landingDuration;
            dragon.transform.position = Vector3.Lerp(startPos, targetPos, landTime);
            yield return null;
        }
        stateMachine.ChangeState(dragon.m_states[Dragon.eState.BattleDragon]);
    }
}
