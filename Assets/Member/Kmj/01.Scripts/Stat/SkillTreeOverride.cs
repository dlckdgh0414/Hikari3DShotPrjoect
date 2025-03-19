using System;
using UnityEngine;

public class SkillTreeOverride : MonoBehaviour
{
    [SerializeField] private FruitsSO _fruits;

    private void Awake()
    {
    }
    public void SetOverrideFruits()
    {
        if(_fruits.soType == FruitsType.HP)
        {
            //_player.HpStat = type.floatValue;
        }
        else if (_fruits.soType == FruitsType.AttackDamage)
        {
            //_player.AttackDamage = type.floatValue;
        }
        else if(_fruits.soType == FruitsType.Speed)
        {
            //_player.speed = type.floatValue;
        }
        else if(_fruits.soType == FruitsType.Skill)
        {
            //��ų�� �ҷ��ͼ� ����� �� �ְڲ� �Ѵ�.
        }
        Debug.Log($"{_fruits.name}");
    }
}
