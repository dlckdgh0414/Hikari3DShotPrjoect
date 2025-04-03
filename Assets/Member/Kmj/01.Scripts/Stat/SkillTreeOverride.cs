using UnityEngine;

public class SkillTreeOverride : MonoBehaviour
{
    [SerializeField] private FruitsSO _fruits;
    private Player _player;
    public void SetOverrideFruits()
    {
        if(_fruits.fruitsType == FruitsType.HP)
        {
            
        }
        else if (_fruits.fruitsType == FruitsType.AttackDamage)
        {
            
        }
        else if(_fruits.fruitsType == FruitsType.Speed)
        {
            
        }
        else if(_fruits.fruitsType == FruitsType.AttackSpeed)
        {

        }
        else if (_fruits.fruitsType == FruitsType.Skill)
        {

        }
        Debug.Log($"{_fruits.name}");
    }
}
