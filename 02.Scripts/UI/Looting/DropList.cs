
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DropList : MonoBehaviour
{
    public ItemDropTable myDropTable;

    public List<LootSlot> myLootSlots;

    public int myLootCount;
    public UnityAction LootLeftCount;
    public bool IsEnterLoot = false;

    // Start is called before the first frame update
    void Start()
    {
        ConfirmItem(myDropTable);
        myLootCount = myLootSlots.Count;
        LootLeftCount += () => myLootCount--;
        //ImageRaycastOff();
    }

    // Update is called once per frame
    void Update()
    {
        if(LootingManager.Inst.LootWindowList != null)
        {
            if (LootingManager.Inst.LootWindowList[0] == this)
            {
                ImageRaycastOn();
            }
        }
        if (myLootCount == 0)
        {
            GameManager.instance.myPlyaer.GetComponent<IinterPlay>().CloseUi?.Invoke();
            LootingManager.Inst.LootWindow = null;
            Destroy(transform.parent.gameObject);
        }
    }

    // 드랍테이블에 있는 아이템을 드랍률체크후 드랍되는아이템을 생성해주는 함수
    void ConfirmItem(ItemDropTable mydropTable)
    {
        for (int i = 0; i < mydropTable.myDropTable.Count(); i++)
        {
            if (LootingManager.Inst.ProbabilityChoose(mydropTable.myDropRate[i]))
            {
                SpawnLootSlot(mydropTable.myDropTable[i]);
            }
        }
    }

    public GameObject LootSlot;

    // 루팅창 내부의 루팅가능한 아이템슬롯을 생성 후 아이템 정보를 추가해주는 함수
    void SpawnLootSlot(ItemStatus itemStatus)
    {
        //GameObject obj = Instantiate(LootSlot, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootSlot") as GameObject, transform);
        obj.GetComponent<LootSlot>().myItem = itemStatus;
        myLootSlots.Add(obj.GetComponent<LootSlot>());
        obj.GetComponent<LootSlot>().myIndex = myLootSlots.Count - 1;
    }

    // 모든아이템을 루팅처리해주는 함수
    public void LootAll()
    {
        for (int i = 0; i < myLootSlots.Count; i++)
        {
            myLootSlots[i].GetComponent<LootSlot>().LootDone();
        }
        GameManager.instance.myPlyaer.GetComponent<IinterPlay>().CloseUi?.Invoke();
        LootingManager.Inst.LootWindow = null;
        Destroy(transform.parent.gameObject);
    }

    // 모든 ImageRaycast를 꺼주는 함수
    public void ImageRaycastOff()
    {
        Image[] myImage = transform.parent.GetComponentsInChildren<Image>();
        foreach (Image img in myImage)
        {
            img.raycastTarget = false;
        }
    }
    // 모든 ImageRaycast를 꺼주는 함수
    public void ImageRaycastOn()
    {
        Image[] myImage = transform.parent.GetComponentsInChildren<Image>();
        foreach(Image img in myImage)
        {
            img.raycastTarget = true;
        }
    }

}
