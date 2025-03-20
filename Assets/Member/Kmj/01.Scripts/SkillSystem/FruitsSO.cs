using System;
using UnityEngine;
using UnityEngine.Serialization;


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

    public FruitsType fruitsType;
    private statType _statType;

    public int intValue { get; set; }
    public float floatValue { get; set; }
    public Skill skillValue { get; set; }

    public string Value;

    private void OnValidate()
    {
        FruitsName = this.name;
        SetValue();
    }

    private void SetValue()
    {
        if (fruitsType == FruitsType.HP || fruitsType == FruitsType.AttackDamage || fruitsType == FruitsType.Speed)
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
        else if (fruitsType == FruitsType.Skill)
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
