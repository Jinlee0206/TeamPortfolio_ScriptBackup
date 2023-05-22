using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverWindow : MonoBehaviour 
{

    public ItemStatus myItem = null;
    public GameObject myObj = null;

    //아이템의 이미지
    public Image myImage = null;

    // 옵션의 이름과 수치의 목록
    public List<TMP_Text> myOptionType;
    public List<TMP_Text> myOptionNum;
    // 옵션의 이름과 수치의 부모
    public Transform Optionparent;
    public Transform OptionNumparent;

    private void Start()
    {
        myImage.sprite = myItem.Image;
        InsertOption(myItem);
        InsertOptionNum(myItem);

    }

    public void MakeOptionText()
    {
        GameObject obj = Instantiate(Resources.Load("UIResource/MouseOverWindow/OP1") as GameObject,Optionparent);
        myOptionType.Add(obj.GetComponent<TMP_Text>());
    }

    public void MakeOptionTexts(int count) 
    {
        for (int i = 0; i < count; i++)
        {
            MakeOptionText();
        }
    }

    public void InsertOption(ItemStatus myItem)
    {
        switch (myItem.MyItemType)
        {
            case ItemType.Weapon:
                MakeOptionTexts(2);
                myOptionType[0].text = "AP : ";
                myOptionType[1].text = "AS : ";
                break;
            case ItemType.Armor:
                MakeOptionTexts(2);
                myOptionType[0].text = "HP : ";
                myOptionType[1].text = "DP : ";
                break;
            case ItemType.Leggins:
                MakeOptionTexts(2);
                myOptionType[0].text = "HP : ";
                myOptionType[1].text = "DP : ";
                break;
            case ItemType.Headgear:
                MakeOptionTexts(2);
                myOptionType[0].text = "HP : ";
                myOptionType[1].text = "DP : ";
                break;
            case ItemType.Boots:
                MakeOptionTexts(3);
                myOptionType[0].text = "HP : ";
                myOptionType[1].text = "DP : ";
                myOptionType[2].text = "Speed : ";
                break;
            case ItemType.Soul:
                //myOptionType[0].text = "HP";
                break;
            default:
                break;
        }
    }

    public void MakeOptionNumText()
    {
        GameObject obj = Instantiate(Resources.Load("UIResource/MouseOverWindow/OPNum") as GameObject, OptionNumparent);
        myOptionNum.Add(obj.GetComponent<TMP_Text>());
    }

    public void MakeOptionNumTexts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            MakeOptionNumText();
        }
    }

    public void InsertOptionNum(ItemStatus myItem)
    {
        switch (myItem.MyItemType)
        {
            case ItemType.Weapon:
                MakeOptionNumTexts(2);
                myOptionNum[0].text = myItem.AttackPoint.ToString();
                myOptionNum[1].text = myItem.AttackSpeed.ToString();
                break;
            case ItemType.Armor:
                MakeOptionNumTexts(2);
                myOptionNum[0].text = myItem.MaxHpIncrese.ToString();
                myOptionNum[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Leggins:
                MakeOptionNumTexts(2);
                myOptionNum[0].text = myItem.MaxHpIncrese.ToString();
                myOptionNum[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Headgear:
                MakeOptionNumTexts(2);
                myOptionNum[0].text = myItem.MaxHpIncrese.ToString();
                myOptionNum[1].text = myItem.DefensePoint.ToString();
                break;
            case ItemType.Boots:
                MakeOptionNumTexts(3);
                myOptionNum[0].text = myItem.MaxHpIncrese.ToString();
                myOptionNum[1].text = myItem.DefensePoint.ToString();
                myOptionNum[2].text = myItem.MoveSpeed.ToString();
                break;
            case ItemType.Soul:
                //myOptionType[0].text = "HP";
                break;
            default:
                break;
        }
    }


}
