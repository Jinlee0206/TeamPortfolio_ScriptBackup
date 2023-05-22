using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IItems  // 다른종류의 슬롯으로이동가능한 아이템만 상속받게 하기
{
    Component myState
    {
        get;
    }

}

public class InventoryManager : Singleton<InventoryManager>
{
    public List<Slot> slots = new();

    // 인벤토리 슬롯 , 아이템 기본틀
    public GameObject inventoryItemPrefab;
    public GameObject inventorySlotPrefab;
    // 최초 인벤토리 슬롯 갯수
    public int startSlotcount = 12;

    // 슬롯들의 부모
    public Transform slotParent;
    private void Awake()
    {
        base.Initialize();
    }

    // 앞에서부터 빈슬롯 확인 후 빈 슬롯에 아이템 추가 / 슬롯이 꽉찻을때 예외처리 및 가독성 작업 완료
    public void AddItem(ItemStatus item)
    {
        int slotIndex = FindEmptySlot();
        if (slotIndex == -1) return;
        else
        {
            SpawnNewItem(item, slots[slotIndex]);
        }
    }

    // 빈 슬롯의 번호 찾기 -1 리턴시 실패
    public int FindEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponentInChildren<Item>() == null)
            {
                return i;
            }
        }
        return -1;
    }

    // 특정 슬롯에 아이템 옮기기
    public void InsertItem(Item item, int SlotNum)
    {
        if (slots[SlotNum].transform.GetComponentInChildren<Item>() == null)
        {
            item.ChangeParent(slots[SlotNum].transform, true);
        }
        else return;
    }

    // 아이템 슬롯 생성
    public void SpawnNewSlot()
    {
        GameObject newSlotGo = Instantiate(inventorySlotPrefab, slotParent);
        newSlotGo.GetComponent<Slot>().mySlotType = ItemSlotType.Inventory;
        slots.Add(newSlotGo.GetComponent<Slot>());
    }

    // 다수의 슬롯 생성
    public void SpawnNewSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnNewSlot();
        }
    }

    //맨 마지막 슬롯 삭제
    public void DeleteSlot()
    {
        Destroy(slots[slots.Count - 1].gameObject);   //인벤토리슬롯 목록의 맨마지막 오브젝트를 제거하고 리스트에서 제거
        slots.RemoveAt(slots.Count - 1);
    }

    // 새로운 아이템 오브젝트 생성
    void SpawnNewItem(ItemStatus item, Slot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);    //아이템 오브젝트 원본 받아와서 슬롯의 자식으로 생성
        newItemGo.GetComponent<Item>().InitialiseItem(item);      //생성한 오브젝트에 InventoryItem 속성 추가 후 item으로 정보주입
        newItemGo.AddComponent<InventoryItem>();
    }


    // 게임 시작시 설정된 숫자만큼 아이템 슬롯 생성하게 하기
    void Start()
    {
        SpawnNewSlots(startSlotcount);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
