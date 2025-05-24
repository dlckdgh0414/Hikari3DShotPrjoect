using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public class TriggerReciver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        GetComponentInParent<NormalBullet>().TriggerPointer(other);
    }
}
