using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterProperty : MonoBehaviour
{
    public UnityAction DeathAlarm;
    public PlayerStatus myBaseStatus;

    [HideInInspector]
    public float MoveSpeed { get { return myBaseStatus.MoveSpeed; } }
    [HideInInspector]
    public float RotSpeed = 360.0f; //1초에 한바퀴.
    public float AttackRange = 1.0f;
    public float AttackDelay = 1.0f;

    [HideInInspector]
    public float playTime = 0.0f;
    public float AttackPoint { get { return myBaseStatus.AttackPoint; }}
    public float DefensePoint { get { return myBaseStatus.DefensePoint;}}
    public float MaxHp { get { return myBaseStatus.MaxHp; } }
    float _curHp = -100.0f; //캐릭터 프로퍼티는 최상위부모. MonoBehaviour가 부모라서 생성자 x,생성자를 이용해서 초기화 X

    public LayerMask enemyLayer;

    [HideInInspector]
    public UnityEvent<float> UpdateHp;
    public float curHp
    {
        get
        {
            if (_curHp < 0.0f) _curHp = MaxHp;
            return _curHp;
        }
        set 
        {
            _curHp = Mathf.Clamp(value, 0.0f, MaxHp);
            UpdateHp?.Invoke(Mathf.Approximately(MaxHp, 0.0f) ? 0.0f : _curHp);
        }
    }
    Animator _anim = null;
    public Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>(); //자기자신의 것부터 찾아봄.
                if (_anim == null) //없으면 자식의 컴포넌트를 가져옴.
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Camera _camera = null;
    protected Camera myCamera //Camera를 가져오는 프로퍼티, 캐릭터 프로퍼티를 상속받는 오브젝트라면 자신의 자식에 있는 Camera를 myCamera로 접근 할 수 있게 된다.
    {
        get
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>(); //자기자신의 것부터 찾아봄.
                if (_camera == null) //없으면 자식의 컴포넌트를 가져옴.
                {
                    _camera = GetComponentInChildren<Camera>();
                }
            }
            return _camera;
        }
    }

    

}
