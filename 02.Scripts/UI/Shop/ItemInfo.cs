using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

// 상점에서 마우스 오버 된 아이템의 정보 출력
public class ItemInfo : MonoBehaviour
{
    // 마우스 오버 된 아이템을 받아올 컴포넌트
    public ItemStatus myItem;

    // 아이템의 이미지 , 이름 , 가격 을 출력 할 오브젝트
    public Image myImage;
    public TMP_Text ItemName;
    public TMP_Text Cost;
    // 가격을 담아둘 상수
    public int myCost;

    // 옵션의 종류 ex ) AP , DP , HP 같은 문자들
    // 옵션의 갯수 , 부모 , 원본  중요!(값 아님)
    public List<TMP_Text> Options = new List<TMP_Text>();
    public Transform Option;
    public GameObject OptionPrefab;
    // 옵션의 종류 출력할 오브젝트 단일생성 MakeOption , 복수생성 MakeOptions
    public void MakeOption()
    {
       
        GameObject Newoptions = Instantiate(OptionPrefab, Option);
        Options.Add(Newoptions.GetComponent<TMP_Text>());
    }
    public void MakeOptions(int count)
    {
        for (int i = 0; i < count; i++)
        {
            MakeOption();
        }
    }
    // 모든 옵션의 종류 오브젝트 삭제
    public void RemoveAllOptions(int count)
    {
        for(int i = 0;i < count; i++)
        {
            Destroy(Options[Options.Count-1].gameObject);
            Options.RemoveAt(Options.Count-1);
        }
    }
    // 옵션의 종류 추가
    public void InsertOption(ItemStatus myItem)
    {
        switch (myItem.MyItemType) 
        {
            case ItemType.Weapon:
                Options[0].text = "AP";
                //Options[1].text = "AS"; 공속 추가시 활성화
                break;
            case ItemType.Armor:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case ItemType.Leggins:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case ItemType.Headgear:
                Options[0].text = "HP";
                Options[1].text = "DP";
                break;
            case ItemType.Boots:
                Options[0].text = "HP";
                Options[1].text = "DP";
                Options[2].text = "Speed";
                break;
            case ItemType.Soul:
                Options[0].text = "HP";
                break;
            default: 
                break;

        }
    }

    // 옵션의 수치 
    // 옵션의 갯수 , 부모 , 원본 
    public List<TMP_Text> OptionNums = new List<TMP_Text>();
    public Transform OptionNum;
    public GameObject OptionNumPrefab;
    // 옵션의 수치 출력할 오브젝트 단일생성 MakeOptionNum , 복수생성 MakeOptionNums
    public void MakeOptionNum()
    {
        GameObject NewoptionNums = Instantiate(OptionNumPrefab, OptionNum);
        OptionNums.Add(NewoptionNums.GetComponent<TMP_Text>());
    }
    public void MakeOptionNums(int count)
    {
        for(int i = 0;i < count; i++)
        {
            MakeOptionNum();
        }
    }
    // 모든 옵션의 수치 오브젝트 삭제 인자로 OptionNums.Count 만 쓰는거 추천
    public void RemoveAllOptionNums(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Destroy(OptionNums[OptionNums.Count - 1].gameObject);
            OptionNums.RemoveAt(OptionNums.Count - 1);
        }
    }
    // 옵션의 수치 스트링 변환후 출력
    public void InsertOptionNums(ItemStatus item)
    {
        switch(item.MyItemType) 
        {
            case ItemType.Weapon:
                OptionNums[0].text = myItem.AttackPoint.ToString();
                //OptionNums[1].text = myItem.AttackSpeed.ToString(); 공속 추가시 활성화
                break;
            case ItemType.Armor:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Leggins:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Headgear:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Boots:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                OptionNums[1].text = myItem.DefensePoint.ToString();
                OptionNums[2].text = myItem.MoveSpeed.ToString();
                break;
            case ItemType.Soul:
                OptionNums[0].text = myItem.MaxHpIncrese.ToString();
                break;
            default:
                break;
        }
    }

    // 아이템 정보 갱신 코루틴 => 코루틴안쓰면 바로갱신이 안됨
    IEnumerator RefreshingItemInfo(ItemStatus item)
    {
        if (item != null)
        {
            myItem = item;
            RemoveAllOptionNums(OptionNums.Count);
            RemoveAllOptions(Options.Count);
            switch (item.MyItemType)
            {
                case ItemType.Weapon:
                    MakeOptions(1);
                    MakeOptionNums(1);
                    break;
                case ItemType.Armor:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case ItemType.Leggins:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case ItemType.Headgear:
                    MakeOptions(2);
                    MakeOptionNums(2);
                    break;
                case ItemType.Boots:
                    MakeOptions(3);
                    MakeOptionNums(3);
                    break;
                case ItemType.Soul:
                    MakeOptions(1);
                    MakeOptionNums(1);
                    break;
                default:
                    break;
            }
            InsertOption(item);
            InsertOptionNums(item);
            yield return new WaitForEndOfFrame();
            ItemName.text = item.ItemName;
            myImage.sprite = item.Image;
            Cost.text = myCost.ToString();
        }
    }

    // 정보갱신 코루팅 가동
    public void RefreshItemInfo(ItemStatus item)
    {
        StartCoroutine(RefreshingItemInfo(item));
    }

    
    // Update is called once per frame
    //void Update()
    //{
        
    //}

}
