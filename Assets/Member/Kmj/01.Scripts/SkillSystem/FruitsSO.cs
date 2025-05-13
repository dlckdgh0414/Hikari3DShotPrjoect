using System;
using UnityEngine;


public enum FruitsType
{
    HP, AttackDamage, Speed, Skill,AttackSpeed
}

public enum statType
{
    Float, Skill
}

[CreateAssetMenu(fileName = "FruitsSO", menuName = "FruitSO")]
public class FruitsSO : ScriptableObject
{
    public string fruitsName;
    public int price;
    public Sprite icon;

    public FruitsType fruitsType;
    private statType _statType;

    public int intValue;
    public float floatValue;
    public Skill skillValue;

    //public Fruits Fruits { get; set; }

    public string Value;

    [TextArea]
    public string description;

    public FruitsSO(FruitsType thisType = FruitsType.HP,string value = "")
    {
        fruitsType = thisType;
        Value = value;
    }

    private void OnValidate()
    {
        fruitsName = this.name;
        SeTValue();
    }

    private void SeTValue()
    {
        if (fruitsType == FruitsType.HP || fruitsType == FruitsType.AttackDamage 
                                        || fruitsType == FruitsType.Speed || fruitsType == FruitsType.AttackSpeed)
        {
            _statType = statType.Float;

            if (Value == string.Empty)
            {
                Debug.Log("³Ê °ª ¾È³ÖÀ½");
            }
            else if(Value != string.Empty)
            {
                floatValue = float.Parse(Value);
            }
        }
        else if (fruitsType == FruitsType.Skill)
        {
            _statType = statType.Skill;
            if (Value == string.Empty)
            {
                Debug.Log("³Ê °ª ¾È³ÖÀ½");
            }
            else
            {
                Type t = Type.GetType(Value);
                Skill skill = Activator.CreateInstance(t) as Skill;
                skillValue = skill;
            }
        }
    }
}