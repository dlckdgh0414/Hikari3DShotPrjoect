using UnityEngine;

public class LookAtUICamera : MonoBehaviour
{
    [SerializeField] private float Zoffset;

    void Update()
    {
        LookAtCamera(Camera.main);
    }

    private void LookAtCamera(Camera main)
    {
       
        Vector3 offsetPos = main.transform.position - main.transform.forward * Zoffset;
        transform.position = offsetPos;

      
        Vector3 lookDirection = transform.position - main.transform.position;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
