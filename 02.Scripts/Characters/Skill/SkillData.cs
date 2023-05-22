using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class SkillData : ScriptableObject
{
    [SerializeField]
    protected Sprite _image;
    public Sprite Image { get { return _image; } }
    [SerializeField]
    protected string _name;
    public string Name { get { return _name; } }
    [SerializeField]
    protected float _value1; //Attack인지 Buff 인지에 따라 Attack-> Damage, Buff-> Buff 수치
    public float Value1 { get { return _value1; } }
    [SerializeField]
    protected float _value2; //Attack인지 Buff 인지에 따라 Attack-> CC기 시간, Buff-> Buff 유지 시간
    public float Value2 { get { return _value2; } }
    [SerializeField]
    protected float _range;
    public float Range { get { return _range; } }
    [SerializeField]
    protected float _distance;
    public float Distance { get { return _distance; } }
    [SerializeField]
    protected float _coolTime;
    public float CoolTime { get { return _coolTime; } }
    [SerializeField]
    protected float _Speed;
    public float Speed { get { return _Speed; } }
    [SerializeField]
    protected CROWNDCONTROLTYPE _crowdControlType;
    public CROWNDCONTROLTYPE CrowdControlType { get { return _crowdControlType; } }
}


public enum CROWNDCONTROLTYPE
{
    None,Burn, Freeze, Electricshock
}