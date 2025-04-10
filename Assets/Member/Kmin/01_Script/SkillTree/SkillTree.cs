using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private List<Fruits> fruitsList;

    private void Awake()
    {
        foreach(var fruits in fruitsList)
        {
            //fruits.Initialize();
        }
    }
}
