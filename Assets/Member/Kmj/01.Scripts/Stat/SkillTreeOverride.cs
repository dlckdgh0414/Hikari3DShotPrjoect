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
            //��ų�� �ҷ��ͼ� ����� �� �ְڲ� �Ѵ�.
        }
        Debug.Log($"{_fruits.name}");
    }
}
