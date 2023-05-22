using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStatus : MonoBehaviour
{
    public List<Slider> slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RefreshBars(int barIndex,float value)
    {
        slider[barIndex].value = value;
    }

}
