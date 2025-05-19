using UnityEngine;
using Unity.Cinemachine;

public class ClampTest : MonoBehaviour
{
    public CinemachineCamera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = camera.transform.eulerAngles;

        target.x = Mathf.Clamp(camera.transform.rotation.x, 10,12);
        target.y = Mathf.Clamp(camera.transform.rotation.x, -2,2);

        camera.transform.rotation = Quaternion.Euler(target);
    }
}
