using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryItem : MonoBehaviour , IItems
{
    public Component myState
    {
        get => this as Component;
    }
    private void Start()
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
    }
    private void OnDestroy()
    {
        //if(transform.parent.GetComponent<Slot>().mySlotItems != null)
        //    transform.parent.GetComponent<Slot>().mySlotItems = null;

    }

}
