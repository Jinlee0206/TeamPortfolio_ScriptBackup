using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackFunc;
    public UnityEvent BiteFunc;
    public UnityEvent DeadFunc;

    public void OnAttack()
    {
        AttackFunc?.Invoke();
    }

    public void OnBite()
    {
        BiteFunc?.Invoke();
    }

    public void OnDead()
    {
        DeadFunc?.Invoke();
    }

    
}
