using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    [SerializeField]
    private List<Material> material = new List<Material>();

    [SerializeField] private MeshRenderer _playerMeshFilert;

    [field : SerializeField] private int currentMaterial;


    public void NextMaterial()
    {
        currentMaterial++;
        if (currentMaterial >= material.Count)
            currentMaterial = 0;

        _playerMeshFilert.material = material[currentMaterial];
    }

    private void Update()
    {
        Debug.Log(material.Count); 
    }

    public void MinusMaterial()
    {
        currentMaterial--;

        if (currentMaterial < 0)
            currentMaterial = material.Count - 1;

        _playerMeshFilert.material = material[currentMaterial];
    }
}
 