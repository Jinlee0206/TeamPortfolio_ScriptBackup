using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_Fly : State
{
    Dragon dragon;

    public DragonState_Fly(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
        
    }

    public override void Enter()
    {
        base.Enter();
        dragon.orgPos = dragon.transform.position;
        dragon.StopAllCoroutines();
        dragon.myAnim.SetTrigger("Fly");
        dragon.myAnim.SetBool("isFlying", true);
        StartFlying();
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

    public void StartFlying()
    {
        dragon.isFlying = true;
        dragon.StartCoroutine(StartFlyingBoost());
    }

    IEnumerator StartFlyingBoost()
    {
        float boostTime = 0f;
        float deltaTime = 0.5f;
        while (boostTime < 1f)
        {
            boostTime += Time.deltaTime;
            dragon.flyPos = new Vector3(dragon.transform.position.x, dragon.flyHeight, dragon.transform.position.z);
            dragon.transform.position = Vector3.Lerp(dragon.transform.position, dragon.flyPos, deltaTime * Time.deltaTime);
            yield return null;
        }
        stateMachine.ChangeState(dragon.m_states[Dragon.eState.FlyToBackPos]);
    }
}
