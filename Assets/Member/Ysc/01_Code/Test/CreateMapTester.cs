using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.Ysc._01_Code.Test
{
    public class CreateMapTester : MonoBehaviour
    {
        [SerializeField] private GameObject target, obj;

        [SerializeField] private float yOffset;
        
        [SerializeField] private int minVal, maxVal;

        private const int range = 5;

        private Rigidbody rb;
        
        private List<GameObject> objs;

        private void Awake()
        {
            objs = new List<GameObject>();
            
            rb = GetComponent<Rigidbody>();
            
            CreateMap();
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = Vector3.back *  10f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CreateMap();
            }
        }
        
        public void CreateMap()
        {
            if (objs.Count > 0)
                ClearObj();
            
            int val = Random.Range(minVal, maxVal);
            
            for (int i = 0; i < val; i++)
            {
                float x = Random.Range(target.gameObject.transform.localScale.x * ( -1 * range), target.gameObject.transform.localScale.x * range);
                float z = Random.Range(target.gameObject.transform.localScale.z * ( -1 * range), target.gameObject.transform.localScale.z * range);
                Vector3 pos = new Vector3(target.transform.position.x + x, target.transform.position.y + yOffset, target.transform.position.z + z);
                objs.Add(Instantiate(obj, pos, Quaternion.identity));
            }
        }

        private void ClearObj()
        {
            foreach (GameObject obj in objs)
            {
                Destroy(obj);
            }
            
            objs.Clear();
        }

        [ContextMenu("DebugLine")]
        public void DebugLine()
        {
            float a = target.transform.position.x;
            float b = target.transform.position.z;
            // Debug.Log(Mathf.Sqrt(a*a + b*b));
            float x = Random.Range(target.gameObject.transform.localScale.x * ( -1 * range), target.gameObject.transform.localScale.x * range);
            float z = Random.Range(target.gameObject.transform.localScale.z * ( -1 * range), target.gameObject.transform.localScale.z * range);
            Debug.Log($"x : {x + target.transform.position.x} \n z : {z + target.transform.position.z}");
        }
    }
}