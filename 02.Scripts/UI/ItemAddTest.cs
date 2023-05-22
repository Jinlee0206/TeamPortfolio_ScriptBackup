using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddTest : MonoBehaviour
{

    public ItemStatus[] itemsList;

    public ItemDropTable[] itemDropTables;

    public void ItemList(int id)
    {
        InventoryManager.Inst.AddItem(itemsList[id]);
    }


}
