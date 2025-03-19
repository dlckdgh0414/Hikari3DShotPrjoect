using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Member.Ysc._01_Code.CameraSystem
{
    public class CameraRender : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        private List<Renderer> targetRendererList;
        
        private void Awake()
        {
            targetRendererList = new List<Renderer>();
            targetRendererList = GetComponentsInChildren<Renderer>().ToList();
        }

        private void LateUpdate()
        {
            VisibleFromCamera(targetRendererList, targetCamera);
        }

        private void VisibleFromCamera(ICollection<Renderer> rendererList, Camera target)
        {
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(target);
            foreach (Renderer ren in rendererList)
            {
                    
                if (GeometryUtility.TestPlanesAABB(frustumPlanes, ren.bounds))
                    ren.gameObject.SetActive(true);
                else
                {
                    if (!ren.gameObject.activeSelf) continue;
                    ren.gameObject.SetActive(false);
                }
            }
        }
    }
}