using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Idle : State
{
    public MonsterState_Idle(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        monster.myAnim.SetBool("isMoving", false);
        if (monster.myTarget == null) monster.StartCoroutine(Roaming(Random.Range(3.0f, 5.0f)));
        else stateMachine.ChangeState(monster.m_states[Monster.eState.Trace]);
    }

    public override void Exit()
    {
        base.Exit();
        monster.StopAllCoroutines();
    }

    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator Roaming(float delay)
    {
        var wfs = new WaitForSeconds(delay);
        yield return wfs;
        Vector3 pos = monster.orgPos;
        pos.x += Random.Range(-5.0f, 5.0f);
        pos.z += Random.Range(-5.0f, 5.0f);
        monster.MoveToPos(pos, () => monster.StartCoroutine(Roaming(Random.Range(3.0f, 5.0f))));
    }

    
}
