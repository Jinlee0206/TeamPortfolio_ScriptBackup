using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler , IPointerClickHandler
{
    // 내 아이템 정보 , 가격 , 이미지
    public ItemStatus myItem;
    public int myCost;
    public Image myImage;
    // 구매버튼
    public Button buyButton;

    // 구매 확정시 인벤토리에 아이템 추가
    public void BuyItem(ItemStatus item,bool isDone)
    {
        if (isDone)
        {
            //Destroy(GetComponent<ShopItem>());
            InventoryManager.Inst.AddItem(item);
        }
    }

    //마우스오버 끝날때
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OFF");
    }

    // 마우스오버 될때
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 상점 정보창 갱신 => 마우스 오버된 상점 아이템의 정보 , 가격 가져와서 넘겨주기
        ShopManager.Inst.itemInfo.RefreshItemInfo
            (eventData.pointerEnter.GetComponent<ShopItem>().myItem);
        ShopManager.Inst.itemInfo.myCost = myCost;

    }

    // 클릭한 아이템을 구매 대상으로 설정하기
    // 추가예정 기능 1. 구매 확인창 뜨게 하기
    //               2. 구매대상 설정시 다른아이템 마우스오버안되게 막기
    public void OnPointerClick(PointerEventData eventData)
    {
        buyButton.gameObject.SetActive(true);
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() =>gameObject.GetComponent<ShopItem>().BuyItem(myItem,true));
    }
}
