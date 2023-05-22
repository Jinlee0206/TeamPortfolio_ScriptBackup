using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickItem : MonoBehaviour , IItems
{
    GameObject myIcon = null;
    public Component myState
    {
        get => this as Component;
    }

    // »ý¼º½Ã ÄðÅ¸ÀÓ ÀÌÆåÆ®¿ë °´Ã¼ 
    private void Start() 
    {
        transform.parent.GetComponent<Slot>().mySlotItems = transform;
        GameObject obj = new("Icon");
        obj.transform.SetParent(transform);
        myIcon = obj;
        obj.AddComponent<SlotCoolDown>();
        obj.AddComponent<Image>();
        obj.GetComponent<SlotCoolDown>().myParent = transform;

    }
    private void OnDestroy()
    {
        Destroy(myIcon);
        transform.GetComponent<Item>().myIcon = null;
        transform.parent.GetComponent<Slot>().mySlotItems = null;
    }

}
