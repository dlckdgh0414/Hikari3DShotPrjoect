using System;
using UnityEngine;

namespace Member.Ysc._01_Code.GameSystem
{
    public class MapSystem : MonoBehaviour
    {
        [SerializeField] private Transform targetTrm;
        [SerializeField] private float offset;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                transform.localPosition += Vector3.back * (-1 * offset);
        }
    }
}