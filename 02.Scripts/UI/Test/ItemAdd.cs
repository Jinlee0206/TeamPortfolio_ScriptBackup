using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdd : MonoBehaviour
{
    public ItemStatus[] itemsList;


    public void ItemList(int id)
    {
        InventoryManager.Inst.AddItem(itemsList[id]);
    }
}
