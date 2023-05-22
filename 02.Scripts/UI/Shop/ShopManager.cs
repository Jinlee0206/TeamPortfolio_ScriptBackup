using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    // 상점목록 스크립트 , 오브젝트 링크
    public ShopList ShopList;
    public Transform myShopList;
    // 판매 아이템 정보 컴포넌트 링크
    public ItemInfo itemInfo;

    // 상점 슬롯 오브젝트 원본 
    public GameObject ShopSlotPrefab;
    public GameObject ShopUI;

    // 구매버튼
    public Button BuyButton;
    // 상점 목록의 종류
    public List<ShopItemList> ShopItemList;


    public void AddListInfo(ItemStatus item)
    {
        
    }
    #region 상점 열기
    // 상점 오픈 ver1 상점 목록을 받아서 열기
    public void OpenShop(ShopItemList itemList)
    {
        ShopUI.SetActive(true);
        ShopManager.Inst.myShopList.GetComponent<ShopList>().myShopItemList = itemList;
        ShopManager.Inst.ShopList.ShopItemRefreshing(itemList);
    }
    //상점 오픈 ver2 상인 타입을 받아서 열기
    public void OpenShop(NpcProperty.NPCType npcType,UnityAction e=null)
    {
        e?.Invoke();
        ShopUI.SetActive(true);
        ShopManager.Inst.myShopList.GetComponent<ShopList>().myShopItemList = ShopItemList[(int)npcType];
        ShopManager.Inst.ShopList.ShopItemRefreshing(ShopItemList[(int)npcType]);
    }
    #endregion

    #region 상점 닫기
    public void CloseShop(UnityAction camera=null,UnityAction interplay=null)
    {
        ShopUI.SetActive(false);
        camera?.Invoke();
        interplay?.Invoke();
    }
    #endregion

    private void Awake()
    {
        base.Initialize();
    }
    private void Start()
    {
        
    }
}
