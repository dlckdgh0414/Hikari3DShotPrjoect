using System;
using UnityEngine;

namespace Member.Ysc._01_Code.GameSystem
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private Material[] matArray;
        
        private MeshRenderer[] _meshRenderers;
        
        private void Awake()
        {
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();
            CreateMap();
        }
        
        private void CreateMap()
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].sharedMaterial = matArray[i];
            }
        }
    }
}