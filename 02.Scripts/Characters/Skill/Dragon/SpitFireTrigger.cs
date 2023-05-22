using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpitFireTrigger : MonoBehaviour
{

    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            other.GetComponent<IBattle>()?.OnDamage(10);
        }
        
    }


}
