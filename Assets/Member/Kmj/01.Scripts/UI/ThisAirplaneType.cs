using System;
using System.Collections.Generic;
using UnityEngine;

public class ThisAirplaneType : MonoBehaviour
{
    [SerializeField] private Custom _customCompo;

    public List<GameObject> airPlane;

    private void Awake()
    {
        airPlane.ForEach(Air => Air.SetActive(false));
    }

    private void Update()
    {
    }
}
