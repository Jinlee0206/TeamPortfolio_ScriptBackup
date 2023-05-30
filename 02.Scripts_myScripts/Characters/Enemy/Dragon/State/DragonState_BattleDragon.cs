using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_BattleDragon : State
{
    Dragon dragon;
    private IEnumerator attackCoroutine;    // 공격 패턴을 담을 코루틴

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

    Coroutine check = null;         // 이미 공격 패턴 실행 중인지 확인하는 코루틴

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        dist = Vector3.Distance(dragon.transform.position, dragon.myTarget.transform.position);

        // 거리 체크를 하는 함수를 구현
        if (!dragon.isFlying && dragon.myTarget != null && dist > dragon.AttackRange + attackRadiusOffset)
        {
            stateMachine.ChangeState(dragon.m_states[Dragon.eState.Trace]);
        }

        // 남은 체력에 비례해서 패턴 선택 분기
        if (dragon.curHp < dragon.MaxHp * 0.2f && !dragon.isBerserk)
        {
            Debug.Log("Test");
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

        // 공격 시야 안에 들어와있는 지 체크
        if (!IsTargetOnSight(dragon.myTarget))
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            dragon.myAnim.SetBool("isMoving", true);

            delta = monster.RotSpeed * Time.deltaTime;
            monster.transform.rotation = Quaternion.Slerp(dragon.transform.rotation, targetRotation, delta / 120.0f);
        }
        else
        {
            dragon.myAnim.SetBool("isMoving", false);
        }
    }

    Vector3 dir;        // 드래곤과 타겟 사이의 벡터
    float delta;        // 회전 Slerp의 델타값

    // 타겟이 시야거리 안에 들어와 있는지 체크
    bool IsTargetOnSight(Transform target)
    {
        dir = target.position - dragon.transform.position;

        if (Vector3.Angle(dir, dragon.transform.forward) > dragon.fieldOfView * 0.5f)
        {
            return false;
        }

        if(Physics.Raycast(dragon.transform.position, dir, out RaycastHit hit, dragon.viewDistance, dragon.enemyLayer))
        {
            if (hit.transform == dragon.myTarget) return true;
        }
        return false;
    }


}
