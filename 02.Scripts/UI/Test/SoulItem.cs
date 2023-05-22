using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }
}
