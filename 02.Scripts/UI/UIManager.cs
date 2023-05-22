using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static UIManager Instance => instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<UIManager>(); // 게임 시작 시 자기 자신을 담음
            DontDestroyOnLoad(this.gameObject);         // 씬이 변경되더라도 자기 자신(싱글톤)을 파괴하지 않고 유지하도록 설정
        }
        else // 이미 유지되고 있는 싱글톤이 있다면
        {
            if (Instance != this) Destroy(this.gameObject); // 씬에 싱글톤 오브젝트가 된 다른 UIManager Object가 있다면 자신을 파괴
        }
        // 각각의 매니저가 존재하지않을때 생성후 참조대상으로 설정해주기
        // 이미 생성되잇을때 재참조기능 추가필요?
        if (FindObjectOfType<InventoryManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/InventorySystem") as GameObject,transform);
            inventoryManager = obj.GetComponent<InventoryManager>();
        }
        if (FindObjectOfType<EquipmentManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/EquipmentSystem") as GameObject, transform);
            equipmentManager = obj.GetComponent<EquipmentManager>();
        }
        if (FindObjectOfType<StatusManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/StatusSystem") as GameObject, transform);
            statusManager = obj.GetComponent<StatusManager>();
        }
        if (FindObjectOfType<LootingManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/LootingSystem") as GameObject, transform);
            lootingManager = obj.GetComponent<LootingManager>();
        }
        if (FindObjectOfType<ShopManager>() == null)
        {
            GameObject obj = Instantiate(Resources.Load("UIResource/System/ShopSystem") as GameObject, transform);
            shopManager = obj.GetComponent<ShopManager>();
        }

    }
    private void Start()
    {
        Refresh();
        skillList = GetComponentInChildren<MySkillList>();
    }
    [Header("Managers")]
    public InventoryManager inventoryManager;
    public EquipmentManager equipmentManager;
    public StatusManager statusManager;
    public ShopManager shopManager;
    public LootingManager lootingManager;
    //참조하는 원본
    //public GameObject inventoryManagerGameObject;
    //public GameObject statusManagerGameObject;
    //public GameObject equipmentManagerGameObject;
    
    public MySkillList skillList;
    public TMP_Text myName;

    public void Refresh()
    {
        StartCoroutine(ManagerSetup());
    }
    IEnumerator ManagerSetup()
    {
        yield return new WaitForFixedUpdate();
        EquipmentManager.Inst.gameObject.SetActive(false);
        InventoryManager.Inst.gameObject.SetActive(false);
        StatusManager.Inst.gameObject.SetActive(false);

    }

    public void TestOpenShop(ShopItemList myList)
    {
        ShopManager.Inst.OpenShop(myList);
    }
}
public enum ItemSlotType
{
    Inventory, Equipment, Quick, Shop, Skill, Soul
}