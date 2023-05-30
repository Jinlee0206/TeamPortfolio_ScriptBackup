using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; protected set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        //Debug.Log(CurrentState);

        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();

        //Debug.Log(newState);
    }
}
