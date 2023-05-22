using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class LootingManager : Singleton<LootingManager>
{

    private void Awake()
    {
        base.Initialize();
    }


    // Start is called before the first frame update
    void Start()
    {
        RefreshLootTarget += OpenLootWindow;
        DelLootTarget += RemoveWindows;
    }

    // Update is called once per frame
    void Update()
    {

    }
    // 함수 호출 대기상태 2종
    public UnityAction<Monster> RefreshLootTarget;
    public UnityAction<Monster> DelLootTarget;

    // 현재 열려잇는 루팅창
    public GameObject LootWindow;

    // myLootingMonster[i] 의 myMonster에 의해 열린 루팅창이 LootingWindowList[i] 의 오브젝트
    // 몬스터종류와 루팅범위진입여부를 담고있는 리스트
    public List<(Monster myMonster,bool IsEnter)> myLootingMonster = new();
    // 열려있는 루팅창의 목록을 담고잇는 리스트
    public List<GameObject> LootWindowList = new();

    // 루팅창 생성방식 및 동작방식이 변경되어 사용불가능
    //public void SpawnLootWindow(ItemDropTable itemDropTable)
    //{
    //    //GameObject obj = Instantiate(LootWindow, transform);
    //    GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
    //    LootWindow = obj;
    //    obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
       
    //}

    // 입력받은 몬스터의 정보로 루팅창 생성하는 함수
    public void ReadyLootWindow(Monster myMonster)
    {
        ItemDropTable itemDropTable = myMonster.myDropTable;
        if (SearchListMonster(myMonster)) return;
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
        //LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        myLootingMonster.Add((myMonster,false));
        LootWindowList.Add(obj);
        //obj.GetComponent<DropList>().IsEnterLoot = true;

        obj.SetActive(false);
    }

    #region myLootWIndows 리스트 탐색 및 수정 함수
    // 루팅가능한 몬스터가 있는지 탐색후 인덱스를 반환하는함수
    public int SearchIndexLootList()
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if (myLootingMonster[i].IsEnter == true)
            {
                return i;
            }
        }
        return -1;
    }
    // 입력받은 몬스터가 루팅창을 생성햇는지 여부를 반환하는 함수
    public bool SearchListMonster(Monster monster)
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if(myLootingMonster[i].myMonster == monster) return true;
        }
        return false;
    }
    // 루팅가능한 몬스터가 존재하는지 반환하는 함수
    public bool SearchLootList()
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if (myLootingMonster[i].IsEnter == true)
            {
                return true;
            }
        }
        return false;
    }
    // 루팅창을 제거하며 몬스터의 삭제 및 리스트의 인덱스까지정리하는 함수
    public void RemoveWindows(Monster monster)
    {
        //int index = -1;
        for (int i = 0; i < LootWindowList.Count; i++)
        {
            if (myLootingMonster[i].myMonster == monster)
            {
                GameManager.instance.myPlyaer.SetIsEnterUI(false);
                Destroy(LootWindowList[i].gameObject);
                myLootingMonster[i].myMonster.ColDelete?.Invoke();
                myLootingMonster[i].myMonster.OnDisappear();
                LootWindowList.RemoveAt(i);
                myLootingMonster.RemoveAt(i);
                break;
            }
        }
    }
    // 몬스터의 루팅가능상태를 갱신시켜주는 함수
    public void ListIsEnterUpdate(Monster monster,bool isEnter)
    {
        for(int i = 0;i < myLootingMonster.Count; i++)
        {
            if (myLootingMonster[i].myMonster == monster)
            {
                myLootingMonster[i] = (monster, isEnter);
            }
        }
    }
    // 해당 몬스터로 인해 열린 루팅창이 존재하면 열어주는 함수
    public void OpenLootWindow(Monster mymonster)
    {
        for (int i = 0; i < myLootingMonster.Count; i++)
        {
            if(myLootingMonster[i].myMonster == mymonster)
            {
                LootWindowList[i].gameObject.SetActive(true);
                //LootWindowList[i].GetComponentInChildren<DropList>().ImageRaycastOn();
                LootWindow = LootWindowList[i].gameObject; 
                break;
            }
        }
    }
    #endregion

    // DropRate Calculate
    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (Random.Range(0, 101) < Rate)
        {
            return true;
        }
        return false;
    }


}
