using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State
{
    protected Monster monster;
    protected StateMachine stateMachine;

    public State(Monster monster, StateMachine stateMachine)
    {
        this.monster = monster;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        Debug.Log($"현재 상태 : {stateMachine.CurrentState}");
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }
}
