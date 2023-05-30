using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpitFireTrigger : MonoBehaviour
{

    public LayerMask enemyMask;
    public SpitFire spitFire;
    ParticleSystem ps = null;
    bool isHIt = false;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            if (!isHIt)
            {
                isHIt = true;
                other.GetComponent<IBattle>()?.OnDamage(spitFire.skillData.Value1);
            }
        }
    }

    //private void OnParticleTrigger()
    //{
    //    List<ParticleSystem.Particle> list = new List<ParticleSystem.Particle>();
    //    int count = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, list, out var data); //data에 부딪힌 오브젝트의 정볻가 담겨져있음.
    //    for(int i = 0; i < count; ++i)
    //    {
    //        if(data.GetCollider(i, 0) == ps.trigger.GetCollider(0)) //ps 트리거의 첫번째 리스트랑 부딪힌 콜라이더와 비교해서 ? 하는? 그런? ㅇㅇ
    //        {
    //            if (!isHIt)
    //            {
    //                //플레이어랑 충돌
    //                isHIt = true;
    //                if (((1 << data.GetCollider(i, 0).gameObject.layer) & enemyMask) != 0)
    //                {
    //                    data.GetCollider(i, 0).gameObject.GetComponent<IBattle>()?.OnDamage(spitFire.skillData.Value1);
    //                }
    //            }
    //        }
    //    }
    //}

}
