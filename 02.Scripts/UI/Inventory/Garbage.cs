using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Garbage : MonoBehaviour , IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if(item != null)
        {
            if(item.GetComponent<Item>().slotType == ItemSlotType.Inventory)
            {
                item.GetComponent<Item>().parentAfterDrag.GetComponent<Slot>().mySlotItems = null;
                Destroy(item.gameObject);
            }
        }   

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
