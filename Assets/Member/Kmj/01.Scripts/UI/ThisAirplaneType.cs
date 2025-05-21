using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThisAirplaneType : MonoBehaviour
{
    [SerializeField] private Custom _customCompo;

    public Dictionary<string, GameObject> airplane = new Dictionary<string, GameObject>();

    private void Awake()
    {
        GetComponentsInChildren<SkinCompo>().ToList().ForEach(child => airplane.Add(child.gameObject.name, child.gameObject));
    }

    private void Start()
    {
        //airplane.ToList().ForEach(Air => Air.Value.SetActive(false));
    }

    private void Update()
    {
        
    }
}
