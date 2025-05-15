using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.Ysc._01_Code.GameSystem
{
    public class CreateMapObject : MonoBehaviour
    {
        private List<GameObject> objList;
        [SerializeField] private Transform targetTrm;

        [SerializeField] private float yOffset;
        [SerializeField] private float zOffset;
        [SerializeField] private float moveSpeed; 
        
        [SerializeField] private int minVal, maxVal;
        

        private const int range = 5;

        private Rigidbody rb;
        
        private List<GameObject> objs;

        private void Awake()
        {
            objs = new List<GameObject>();
            
            rb = GetComponent<Rigidbody>();
            
        }

        private void Start()
        {
            CreateObj();
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = Vector3.back *  moveSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                transform.position = targetTrm.position + new Vector3(0, 0, zOffset);
                CreateObj();
            }
        }
        
        private void CreateObj()
        {
            
            if (objs.Count > 0)
                ClearObj();
            
            if (objList == null) return;
            
            int val = Random.Range(minVal, maxVal+1);
            
            for (int i = 0; i < val; i++)
            {
                float x = Random.Range(gameObject.transform.localScale.x * ( -1 * range), gameObject.transform.localScale.x * range);
                float z = Random.Range(gameObject.transform.localScale.z * (-1 * range), gameObject.transform.localScale.z * range);
                Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + yOffset,transform.position.z + z);
                GameObject o = Instantiate(objList[Random.Range(0, objList.Count)], pos, Quaternion.identity);
                o.transform.SetParent(transform);
                objs.Add(o);
            }
        }

        private void ClearObj()
        {
            foreach (GameObject o in objs)
            {
                Destroy(o);
            }
            
            objs.Clear();
        }

        [ContextMenu("DebugLine")]
        public void DebugLine()
        {
            float a = transform.position.x;
            float b = transform.position.z;
            // Debug.Log(Mathf.Sqrt(a*a + b*b));
            float x = Random.Range(gameObject.transform.localScale.x * ( -1 * range), gameObject.transform.localScale.x * range);
            float z = Random.Range(gameObject.transform.localScale.z * ( -1 * range), gameObject.transform.localScale.z * range);
            Debug.Log($"x : {x + transform.position.x} \n z : {z + transform.position.z}");
        }
    }
}