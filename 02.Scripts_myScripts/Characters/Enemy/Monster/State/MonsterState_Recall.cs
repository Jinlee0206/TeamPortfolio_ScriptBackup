using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState_Recall : State
{
    public MonsterState_Recall(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.MoveToPos(monster.orgPos, () =>
        {
            if (!monster.myAnim.GetBool("isAttacking")) stateMachine.ChangeState(monster.m_states[Monster.eState.Idle]);
        });
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
}
