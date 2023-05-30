using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCutScene : MonoBehaviour
{
    public LayerMask playerMask;
    //bool flag = false;

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Y))
        //{
        //    SceneLoader.Inst.SceneLoadAdditive(8);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask) != 0)
        {
            SceneLoader.Inst.SceneLoadAdditive(8);
        }
    }
}
