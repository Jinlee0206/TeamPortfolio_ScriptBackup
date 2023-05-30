using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterMovement_V2 : CharacterProperty
{
    #region 유니티 이벤트 함수

    // Awake 함수 추가
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void LateUpdate()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    #endregion

    #region Move
    // 이동 동작 - 플레이어, 몬스터 둘다 사용
    // 추상 함수로 선언 -> 각 플레이어, 에너미 스크립트에서 override로 정의 필요
    public abstract void MoveToPos(Vector3 pos, UnityAction done = null); // 수민 스크립트의 Move() 함수 변형하면 될 것으로 보임
    #endregion

    #region Attack

    public void Attack(Transform attackPoint)
    {
        Collider[] list = Physics.OverlapSphere(attackPoint.position, 0.75f, enemyLayer); //웨폰 포인트는 캐릭터 무기의 위치를 가지고 있음.
                                                                                          //  그 위치의 0.75 반지름을 가진 구의 공격범위를 가지고 있다. (나중에 수정 예정)
                                                                                          // overLap에 감지된 콜라이더들을 list에 저장.
        Debug.DrawLine(attackPoint.position, attackPoint.position + new Vector3(0.5f, 0.5f, 0.5f)); //이건 범위를 확인하기 위한 DrawLine (나중에 지울 예정)
        foreach (Collider col in list) //list의 콜라이더들을 하나하나 꺼내서 IBattle 컴포넌트를 가지고 있는지 확인하고 맞으면 => OnDamage 함수 실행.
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

    //// 플레이어는 
    //public abstract void Attack(Transform target = null);


    #endregion



}
