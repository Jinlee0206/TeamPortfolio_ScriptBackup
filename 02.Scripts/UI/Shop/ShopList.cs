using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    // 선택한 아이템 정보 담고있을예정 , 필요가능성이 있어서 남겨놓음
    //public ShopItem Selectitems;
    //public ShopSlot SelectShopItems;
    //public GameObject ShopSlotPrefab;

    // 상점 아이템 오브젝트 원본
    public GameObject ShopItemPrefab;

    // 현재 열린 상점의 목록
    public ShopItemList myShopItemList;
    // 상점의 슬롯 목록
    public List<ShopSlot> shopSlots;

    // 게임 실행시 상점창 갱신 및 비활성화 상태로 전환
    // 기본목록을 보유하고있어야 레퍼런스 오류가안나서 추가해둠
    private void Start()
    {
        ShopItemRefreshing(myShopItemList);
        ShopManager.Inst.ShopUI.SetActive(false);
    }

    // 입력받은 숫자만큼 뒤에서부터 상점 슬롯 제거
    public void ResetSlots(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Destroy(shopSlots[shopSlots.Count-1].GetComponentInChildren<ShopItem>().gameObject);
            Destroy(shopSlots[shopSlots.Count - 1].gameObject);
            shopSlots.RemoveAt(shopSlots.Count - 1);
        }
    }
    // 상점 슬롯 단일추가 ShopSlotAdd , 복수추가 SlotAddItems
    public void ShopSlotAdd()
    {
        GameObject obj = Instantiate(ShopManager.Inst.ShopSlotPrefab, transform);
        shopSlots.Add(obj.GetComponent<ShopSlot>());
    }
    public void SlotAddItems(int index)
    {
        GameObject obj = Instantiate(ShopManager.Inst.ShopList.ShopItemPrefab,
            shopSlots[index].transform);
        obj.GetComponent<ShopItem>().myItem = myShopItemList.items[index];
        obj.GetComponent<ShopItem>().myCost = myShopItemList.cost[index];
        obj.GetComponent<ShopItem>().myImage.sprite = myShopItemList.items[index].Image;
        obj.GetComponent<ShopItem>().buyButton = ShopManager.Inst.BuyButton;
    }

    //상점 목록 갱신코루틴 상점목록 스크립터블 오브젝트를 입력받음
    IEnumerator ShopItemRefresh(ShopItemList shopItemList)
    {
        ResetSlots(shopSlots.Count);
        yield return new WaitForEndOfFrame();
        for(int i = 0;i < shopItemList.items.Count;i++)
        {
            ShopSlotAdd();
            SlotAddItems(i);
        }
    }
    // 상점 목록 갱신 코루틴 스크립터블 오브젝트를 입력받고 코루틴에 전달
    public void ShopItemRefreshing(ShopItemList shopItemList)
    {
        StartCoroutine(ShopItemRefresh(shopItemList));
    }

    
}
