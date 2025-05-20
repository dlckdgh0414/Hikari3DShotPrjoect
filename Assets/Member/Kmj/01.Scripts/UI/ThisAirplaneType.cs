using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThisAirplaneType : MonoBehaviour
{
    [SerializeField] private Custom _customCompo;

    public Dictionary<string, GameObject> airplane;

    private void Awake()
    {
        airplane.ToList().ForEach(Air => Air.Value.SetActive(false));
    }

    private void Update()
    {
    }
}
