using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    public TMP_Text[] myStat;


    public void RefreshingStat()
    {
        for (int i = 0; i < myStat.Length; i++)
        {
            switch ((StatType)i)
            {
                case StatType.HP:
                    myStat[i].text = GameManager.instance.myPlyaer.myBaseStatus.MaxHp.ToString()+ $"(+{EquipmentManager.Inst.equipmentHP.ToString()})";
                    break;
                case StatType.AP:
                    myStat[i].text = GameManager.instance.myPlyaer.myBaseStatus.AttackPoint.ToString()+ $"(+{EquipmentManager.Inst.equipmentAP.ToString()})";
                    break;
                case StatType.DP:
                    myStat[i].text = GameManager.instance.myPlyaer.myBaseStatus.DefensePoint.ToString() + $"(+{EquipmentManager.Inst.equipmentDP.ToString()})";
                    break;
                case StatType.Speed:
                    myStat[i].text = (GameManager.instance.myPlyaer.myBaseStatus.MoveSpeed /3 ).ToString("F1") + $"(+{(EquipmentManager.Inst.equipmentSpeed/3).ToString("F1")})";
                    break;
                //case StatType.AS:
                //    myStat[i].text = Gamemanager.instance.myPlyaer.AttackDelay.ToString() + $"(+{EquipmentManager.Inst.equipmentAS.ToString()})";
                //    break;
            }
        }
    }
}
public enum StatType
{
    HP, AP, DP, Speed, AS
}
