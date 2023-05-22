using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//우측 상단 메뉴 스크립트
public class ToggleButton : MonoBehaviour 
{
    public Button mybutton;

    //메뉴 목록
    public List<GameObject> mySystems;
    // Start is called before the first frame update
    void Start()
    {
        //메뉴목록 1.인벤토리 2.장비창 3.스테이터스 4.소울강화(추가예정)
        mybutton = GetComponent<Button>();
        mySystems.Add(InventoryManager.Inst.gameObject);
        mySystems.Add(EquipmentManager.Inst.gameObject);
        mySystems.Add(StatusManager.Inst.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if(mybutton.enabled)
        //{
        //    if (gameObject.activeSelf)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        gameObject.SetActive(true);
        //    }
        //}
    }
    public void ToggleBUtton(Button button)
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // 메뉴창 껏다켜기
    public void OnbuttonClick(int index)
    {
        mySystems[index].gameObject.SetActive(!mySystems[index].gameObject.activeSelf);
        //if (mySystems[index].gameObject.activeSelf)
        //{
        //    mySystems[index].gameObject.SetActive(false);
        //}
        //else
        //{
        //    mySystems[index].gameObject.SetActive(true);
        //}
    }

}
