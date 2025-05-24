using System;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public class NormalBullet : BaseBullet
    {
        protected override void BulletInit()
        {
            base.BulletInit();
            isRotModle = true;
        }
        public void TriggerPointer(Collider other)
        {
            Hit(other);
            DestroyBullet(this);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Hit(other);
                DestroyBullet(this);
            }
        }
    }
}