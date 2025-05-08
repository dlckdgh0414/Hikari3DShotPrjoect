using UnityEngine;
using Unity.Cinemachine;

public class TestCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
