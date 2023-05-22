using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LootSlot : MonoBehaviour , IPointerClickHandler ,IPointerEnterHandler , IPointerExitHandler ,IPointerMoveHandler
{
    public ItemStatus myItem;

    public TMP_Text myItemName;
    public Image myItemImage;

    public UnityAction myAction;
    public Sprite emptyImage;
    public int myIndex = -1;
    bool isLoot = false;
    // Start is called before the first frame update
    // 주입받은 myItem 정보를 기준으로 슬롯 내부데이터 갱신
    void Start()
    {
        myItemName = GetComponentInChildren<TMP_Text>();
        myItemImage.sprite = GetComponentInChildren<Image>().sprite;
        myItemName.text = myItem.ItemName;
        myItemImage.sprite = myItem.Image;
        myAction += LootDone;
    }

    // 클릭으로 아이템 획득 기능
    public void LootDone()
    {
        // 인벤토리가 비어있다면
        if(InventoryManager.Inst.FindEmptySlot() != -1)
        {
            myItemImage.sprite = emptyImage;
            myItemName.text = null;
            InventoryManager.Inst.AddItem(myItem);
            transform.parent.GetComponent<DropList>().LootLeftCount?.Invoke();
            isLoot = true;
        }
        // 가득차있다면 인벤토리 풀 이벤트가 발생하게 할예정
        else
        {

            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myAction?.Invoke();
        if(MouseOverWindows != null)
        {
            Destroy(MouseOverWindows.gameObject);   
        }
    }
    public GameObject MouseOverWindows;
    // 마우스가 올라오면 팝업창이 뜨게 하기
    // 마우스의 좌측이나 우측 상단에 팝업창이 뜨고 위치가 유지되도록 하기
    Vector2 mousePos = Vector2.zero;
    Vector2 sumPos = Vector2.zero;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(MouseOverWindows == null)
        {
            if(isLoot == false)
            {
                MouseOverWindows = Instantiate(Resources.Load("UIResource/MouseOverWindow/MouseOverWindow") as GameObject,
                    LootingManager.Inst.LootWindow.transform);
                MouseOverWindows.GetComponent<MouseOverWindow>().myItem = myItem;
                mousePos = (Vector2)transform.position - eventData.position;
                sumPos = new Vector2(MouseOverWindows.GetComponent<RectTransform>().sizeDelta.x / 2,
                    MouseOverWindows.GetComponent<RectTransform>().sizeDelta.y / 2);
                MouseOverWindows.transform.position = eventData.position + sumPos;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(MouseOverWindows != null)
        {
            if (isLoot == false)
            {

                Destroy(MouseOverWindows);
                MouseOverWindows = null;
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isLoot == false)
        {
            MouseOverWindows.transform.position = eventData.position + sumPos;
        }

        Debug.Log(eventData.position);
    }
}
