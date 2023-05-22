using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Transform mySlotItems;
    public ItemSlotType mySlotType;
    public ItemType myItemType;

    public ItemSlotType tempSlotType;

    public void OnDrop(PointerEventData eventData)
    {
        Item myItem = transform.GetComponentInChildren<Item>();
        Item dropItem = eventData.pointerDrag.GetComponent<Item>();
        int SlotNum = InventoryManager.Inst.FindEmptySlot();

        if (dropItem.slotType == ItemSlotType.Shop) return;  // 상점아이템일경우는 아무동작하지않게 하기
        if (myItem == null) // 빈 슬롯일때
        {
            if (dropItem.GetComponent<IItems>() != null) // 이동가능한 아이템일경우
            {
                
                switch (mySlotType)
                {
                    case ItemSlotType.Inventory:
                        dropItem.parentAfterDrag = transform;
                        Destroy(dropItem.GetComponent<IItems>().myState);
                        dropItem.AddComponent<InventoryItem>();
                        dropItem.slotType = ItemSlotType.Inventory;
                        myItemType = dropItem.item.MyItemType;
                        break;
                    case ItemSlotType.Equipment:
                        if ((dropItem.item.actionType == ActionType.Attack || dropItem.item.actionType == ActionType.Defense)&&
                            (myItemType == dropItem.item.MyItemType))
                        {
                            dropItem.parentAfterDrag = transform;
                            Destroy(dropItem.GetComponent<IItems>().myState);
                            dropItem.AddComponent<EquipmentItem>();
                            dropItem.slotType = ItemSlotType.Equipment;
                            myItemType = dropItem.item.MyItemType;
                        }
                        break;
                    case ItemSlotType.Quick:
                        if (dropItem.item.actionType == ActionType.UsingItems)
                        {
                            dropItem.parentAfterDrag = transform;
                            Destroy(dropItem.GetComponent<IItems>().myState);
                            dropItem.AddComponent<QuickItem>();
                            dropItem.slotType = ItemSlotType.Quick;
                            myItemType = dropItem.item.MyItemType;
                        }
                        break;
                    case ItemSlotType.Soul:
                        if (dropItem.item.actionType == ActionType.Inchent)
                        {
                            dropItem.parentAfterDrag = transform;
                            Destroy(dropItem.GetComponent<IItems>().myState);
                            dropItem.AddComponent<SoulItem>();
                            dropItem.slotType = ItemSlotType.Soul;
                            myItemType = dropItem.item.MyItemType;
                        }
                        break;
                }
            }
        }
        else // 빈슬롯이 아닐때
        {
            // 1. 인벤토리내에서는 상관없이 이동
            // 장비창 <-> 인벤토리 , 퀵슬롯 <-> 인벤토리 , 인벤토리 <-> 소울 간 이동만 가능하게 설정

            if (dropItem.GetComponent<IItems>() != null)  // 이동 가능한 아이템일경우
            {
                if ((mySlotType == ItemSlotType.Inventory) ^ (dropItem.slotType == ItemSlotType.Inventory)) // 현재 슬롯이나 옮기는 아이템 둘중하나가 인벤토리일경우
                {
                    if ((mySlotType == ItemSlotType.Equipment) ^ (dropItem.slotType == ItemSlotType.Equipment)) // 다른한쪽은 장비창일때 속성비교
                    {
                        if (myItem.item.MyItemType == dropItem.item.MyItemType)
                        {
                            tempSlotType = myItem.slotType;
                            myItem.slotType = dropItem.slotType;
                            dropItem.slotType = tempSlotType;
                        }
                        else
                        {
                            return; // 다른 속성일때는 종료
                        }
                    }
                    else if ((mySlotType == ItemSlotType.Quick) ^ (dropItem.slotType == ItemSlotType.Quick)) // 퀵슬롯일때 동작
                    {
                        if (dropItem.slotType == ItemSlotType.Quick)
                        {
                            tempSlotType = myItem.slotType;
                            myItem.slotType = dropItem.slotType;
                            dropItem.slotType = tempSlotType;
                        }
                        else return;
                    }
                    else if ((mySlotType == ItemSlotType.Soul) ^ (dropItem.slotType == ItemSlotType.Soul)) // 소울창일때 동작
                    {
                        if (dropItem.slotType == ItemSlotType.Soul)
                        {
                            tempSlotType = myItem.slotType;
                            myItem.slotType = dropItem.slotType;
                            dropItem.slotType = tempSlotType;
                        }
                        else return;
                    }
                    Destroy(dropItem.GetComponent<IItems>().myState);
                    Destroy(myItem.GetComponent<IItems>().myState);
                    AddItemType(dropItem.slotType, dropItem);
                    AddItemType(myItem.slotType, myItem);
                    myItem.ChangeParent(dropItem.parentAfterDrag, true);
                    dropItem.ChangeParent(transform);
                }
            }
        }

    }

    public void AddItemType(ItemSlotType type, Item item)
    {
        switch (type) 
        {
            case ItemSlotType.Inventory:
                item.transform.AddComponent<InventoryItem>();
                break;
            case ItemSlotType.Equipment:
                item.transform.AddComponent<EquipmentItem>();
                break;
            case ItemSlotType.Quick:
                item.transform.AddComponent<QuickItem>();
                break;
            case ItemSlotType.Soul:
                item.transform.AddComponent<SoulItem>();
                break;
            default:
                break;
        }
    }

}
