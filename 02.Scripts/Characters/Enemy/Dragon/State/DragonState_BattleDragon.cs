using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_BattleDragon : State
{
    Dragon dragon;
    private IEnumerator attackCoroutine;

    float dist;
    float attackRadiusOffset = 0.05f;

    public DragonState_BattleDragon(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
        dragon = monster as Dragon;
        attackCoroutine = null;
    }

    public override void Enter()
    {
        base.Enter();
        dragon.StopAllCoroutines();
        Debug.Log("DragonBattleState");
        if (check != null) check = dragon.StartCoroutine(attackCoroutine);
    }

    public override void Exit()
    {
        base.Exit();
        dragon.StopCoroutine(attackCoroutine);
    }

    Coroutine check = null;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dist = Vector3.Distance(dragon.transform.position, dragon.myTarget.transform.position);

        // 거리 체크를 하는 함수를 구현
        if (!dragon.isFlying && dragon.myTarget != null && dist > dragon.AttackRange + attackRadiusOffset)
        {
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Trace]);
        }

        if (dragon.curHp < dragon.MaxHp * 0.2f && !dragon.isBerserk)
        {
            dragon.isBerserk = true;
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Fly]);
        }
        else if (check == null)
        {
            attackCoroutine = dragon.pattern.DoAttackPattern();
            check = dragon.StartCoroutine(attackCoroutine);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
