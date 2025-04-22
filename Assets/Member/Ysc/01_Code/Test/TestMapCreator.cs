using UnityEngine;
using UnityEngine.Rendering;

namespace Member.Ysc._01_Code.Test
{
    public class TestMapCreator : MonoBehaviour
    {
        [SerializeField] private Material[] matArray;

        private MeshFilter _meshFilter;
        private MeshRenderer[] _meshRenderers;
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }

        private void Start()
        {
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