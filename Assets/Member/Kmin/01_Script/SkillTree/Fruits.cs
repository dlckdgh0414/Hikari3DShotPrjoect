using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Fruits : MonoBehaviour
{
    [SerializeField] private FruitsSO fruitsSO;

    public bool IsActive { get; private set; }
    
    private List<Fruits> fruitsList;
    private Button _fruitsBtn;

    private void Awake()
    {
        _fruitsBtn = GetComponentInChildren<Button>();
    }

}
