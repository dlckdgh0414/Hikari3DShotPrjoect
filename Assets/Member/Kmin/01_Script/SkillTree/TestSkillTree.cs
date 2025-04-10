using System.Collections.Generic;
using UnityEngine;

public class TestSkillTree : MonoBehaviour
{
    [SerializeField] private List<Fruits> fruitsList;
    [SerializeField] private SkillTreeSO skillTreeSO;

    private void Awake()
    {
        fruitsList.ForEach(f => f.Initialize());
    }
}
