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
            //스킬을 불러와서 사용할 수 있겠끔 한다.
        }
        Debug.Log($"{_fruits.name}");
    }
}
