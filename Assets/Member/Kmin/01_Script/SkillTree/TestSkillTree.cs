using System.Collections.Generic;
using UnityEngine;

public class TestSkillTree : MonoBehaviour
{
    [SerializeField] private List<Fruits> fruitsList;
    [SerializeField] private SkillTreeSO skillTreeSO;
    [SerializeField] private GameEventChannelSO eventChannelSO;
    
    private SkillTreeEvent _skillTreeEvent = SkillTreeEventChannel.SkillTreeEvent;

    private void Awake()
    {
        fruitsList.ForEach(f => f.Initialize());

        foreach (Fruits f in fruitsList)
        {
            f.FruitsButton.onClick.AddListener(() => SelectFruits(f.GetFruitsSO()));
        }
    }
    
    
    public void SelectFruits(FruitsSO selectedFruits)
    {
        _skillTreeEvent.fruitsSO = selectedFruits;
        eventChannelSO.RaiseEvent(_skillTreeEvent);
    }
}
