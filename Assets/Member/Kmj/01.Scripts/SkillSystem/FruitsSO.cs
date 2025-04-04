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

    [SerializeField] private string FruitsName;

    public FruitsType fruitsType;
    private statType _statType;

    public int intValue { get; set; }
    public float floatValue { get; set; }
    public Skill skillValue { get; set; }

    public string Value;

    public FruitsSO(FruitsType thisType = FruitsType.HP,string value = "")
    {
        fruitsType = thisType;
        Value = value;
    }

    private void OnValidate()
    {
        FruitsName = this.name;
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