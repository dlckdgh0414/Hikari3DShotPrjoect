using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public class RocketBullet : BaseBullet
    {
        protected override void BulletInit()
        {
            base.BulletInit();
            isRotModle = true;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("앙 플레이어띠");
                Hit(other);
                DestroyBullet(this);
            }
        }
    }
}