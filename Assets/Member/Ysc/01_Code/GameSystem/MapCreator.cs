using UnityEngine;
using UnityEngine.Rendering;

namespace Member.Ysc._01_Code.GameSystem
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private GameObject map;
        [SerializeField] private Material[] matArray;

        private MeshFilter _meshFilter;
        private MeshRenderer[] _meshRenderers;
        private void Awake()
        {
            _meshFilter = map.GetComponent<MeshFilter>();
            _meshRenderers = map.GetComponentsInChildren<MeshRenderer>();
            _meshFilter.mesh = new Mesh();
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

            CombineMesh();
        }

        private void CombineMesh()
        {
            MeshFilter[] meshFilters = map.GetComponentsInChildren<MeshFilter>(false);
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            
            int vertexCount = 0;

            for (int i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].sharedMesh == null) continue;
                
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                
                meshFilters[i].gameObject.SetActive(false);
                
                vertexCount += meshFilters[i].sharedMesh.vertexCount;
            }
            _meshFilter.mesh = new Mesh();
            if (vertexCount > 65535)
                _meshFilter.mesh.indexFormat = IndexFormat.UInt32;

            
            _meshFilter.mesh.CombineMeshes(combine);
            map.gameObject.SetActive(true);
        }
    }
}