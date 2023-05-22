using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterState_Die : State
{
    public MonsterState_Die(Monster monster, StateMachine stateMachine) : base(monster, stateMachine)
    {
    }
    /// <summary>
    /// 사망상태 진입시 동작 
    /// 1. 몬스터의 콜라이더 삭제 대기열 등록
    /// 2. 사망 애니메이션 출력
    /// 3. 드랍테이블에 맞는 루팅창 생성 및 비활성화
    /// 4. AIPerception 위치에 LootingPerception 컴포넌트 추가
    /// 5. DeathAlarm 실행
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        monster.OnDie();
        monster.StopAllCoroutines();
        Transform transform = monster.transform;
        monster.ColDelete += DisableCollider;
        monster.myAnim.SetTrigger("Die");
        LootingManager.Inst.ReadyLootWindow(monster);
        //Object.Destroy(transform.GetComponentInChildren<AIPerception>());
        //monster.myAI.AddComponent<LootingPerception>();
        monster.DeathAlarm?.Invoke();
        //monster.OnDisappear();
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

    void DisableCollider()
    {
        Collider[] list = monster.transform.GetComponentsInChildren<Collider>();
        foreach (Collider col in list) col.enabled = false;
    }

    

    

}
