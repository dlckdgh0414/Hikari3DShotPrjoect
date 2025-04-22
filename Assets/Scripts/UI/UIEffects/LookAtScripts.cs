using UnityEngine;

public class LookAtScripts : MonoBehaviour
{
    Vector3 dir;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(transform.position, Camera.main.transform.position);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = worldPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(worldPos);
        rotation *= Quaternion.Euler(0, 0, 0); 
        transform.rotation = rotation;
    }
}
