using UnityEngine;

public class SkillTreeOverride : MonoBehaviour
{
    [SerializeField] private FruitsSO _fruits;
    public void SetOverrideFruits()
    {
        if(_fruits.soType == FruitsType.HP)
        {
            //_player.HpStat = _fruits.floatValue;
        }
        else if (_fruits.soType == FruitsType.AttackDamage)
        {
            //_player.AttackDamage = _fruits.floatValue;
        }
        else if(_fruits.soType == FruitsType.Speed)
        {
            //_player.speed = _fruits.floatValue;
        }
        else if(_fruits.soType == FruitsType.Skill)
        {
            //스킬을 불러와서 사용할 수 있겠끔 한다.
        }
        Debug.Log($"{_fruits.name}");
    }
}
