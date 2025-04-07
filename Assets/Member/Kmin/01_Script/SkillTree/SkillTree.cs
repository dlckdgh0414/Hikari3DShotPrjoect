using System.Collections.Generic;
using UnityEngine;

public abstract class SkillTree : MonoBehaviour
{
    [SerializeField] private List<Fruits> fruitsList;

    protected abstract void Initialize();
}
