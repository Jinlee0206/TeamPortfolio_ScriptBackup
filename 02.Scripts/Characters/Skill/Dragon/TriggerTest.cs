using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name}");
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{other.gameObject.name}");
    }
}
