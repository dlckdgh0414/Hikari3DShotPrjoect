using System;
using UnityEngine;


public enum FruitsType
{
    HP, AttackDamage, Speed, Skill
}

public enum statType
{
    Float, Skill
}


[CreateAssetMenu(fileName = "FruitsSO", menuName = "FruitSO")]
public class FruitsSO : ScriptableObject
{

    [SerializeField] private string FruitsName;

    public FruitsType soType;
    private statType _statType;

    public int intValue { get; set; }
    public float floatValue { get; set; }
    public Skill skillValue { get; set; }


    public string Value;

    public void Init(FruitsType thisType = FruitsType.HP,string value = "")
    {
        soType = thisType;
        Value = value;
    }

    private void OnEnable()
    {
    }

    private void OnValidate()
    {
        FruitsName = this.name;
        SeTValue();
    }

    private void SeTValue()
    {
        if (soType == FruitsType.HP || soType == FruitsType.AttackDamage || soType == FruitsType.Speed)
        {
            _statType = statType.Float;

            if (Value == string.Empty)
            {
                Debug.Log("�� �� �ȳ���");
            }
            else if(Value != string.Empty)
            {
                floatValue = float.Parse(Value);
            }
        }
        else if (soType == FruitsType.Skill)
        {
            _statType = statType.Skill;
            if (Value == string.Empty)
            {
                Debug.Log("�� �� �ȳ���");
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
