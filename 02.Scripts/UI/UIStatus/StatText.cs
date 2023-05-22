using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatText : MonoBehaviour
{
    public TMP_Text text;

    //public StatType type;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        StatusManager.Inst.RefreshingStat();
    }
}
