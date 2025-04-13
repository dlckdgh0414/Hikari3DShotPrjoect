using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Custom : MonoBehaviour
{
    [SerializeField]
    private List<Material> material = new List<Material>();

    [SerializeField] private MeshRenderer _playerMeshFilert;

    private int currentMaterial;

    public void NextMaterial()
    {
        currentMaterial++;
        _playerMeshFilert.material = material[currentMaterial];
    }

    public void MinusMaterial()
    {
        currentMaterial--;
        _playerMeshFilert.material = material[currentMaterial];
    }
}
 