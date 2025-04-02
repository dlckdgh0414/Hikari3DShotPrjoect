using UnityEngine;

public class SkillTreeOverride : MonoBehaviour
{
    [SerializeField] private FruitsSO _fruits;
    private Player _player;
    public void SetOverrideFruits()
    {
        if(_fruits.soType == FruitsType.HP)
        {
            
        }
        else if (_fruits.soType == FruitsType.AttackDamage)
        {
            
        }
        else if(_fruits.soType == FruitsType.Speed)
        {
            
        }
        else if(_fruits.soType == FruitsType.AttackSpeed)
        {

        }
        else if (_fruits.soType == FruitsType.Skill)
        {

        }
        Debug.Log($"{_fruits.name}");
    }
}
