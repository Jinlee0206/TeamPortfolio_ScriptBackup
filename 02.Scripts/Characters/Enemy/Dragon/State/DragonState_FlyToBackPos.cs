using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_FlyToBackPos : State
{
    Dragon dragon;

    public DragonState_FlyToBackPos(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
        dragon.StartCoroutine(FlyToBackPos(dragon.flyToBackPos.position));
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

    IEnumerator FlyToBackPos(Vector3 pos)
    {
        Vector3 dir = pos - dragon.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        dragon.StartCoroutine(Rotating(dir));

        dragon.myAnim.SetBool("isFlyingNormal", true);

        while (dist > 0.0f)
        {
            float delta = dragon.flySpeed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            dragon.transform.Translate(dir * delta, Space.World);

            yield return null;
        }

        dragon.myAnim.SetBool("isFlyingNormal", false);
        dragon.StartCoroutine(Rotating(-dir));
        stateMachine.ChangeState(dragon.m_states[Dragon.eState.FlySpitFire]);
    }


    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(dragon.transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(dragon.transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        while (angle > 0.0f)
        {
            float delta = dragon.RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            dragon.transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }

}
