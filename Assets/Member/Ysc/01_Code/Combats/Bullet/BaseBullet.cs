using System;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combats;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [field: SerializeField] public BulletSettingSO BulletSO { get; protected set; } // 총알 데이터 받기
        
        public Rigidbody RbCompo { get; protected set; }
        
        protected Vector3 fireDirection;
        
        public void SetDirection(Vector3 direction)
        {
            fireDirection = direction - transform.position ;
        }
        
        protected void Awake()
        {
            BulletInit();
        }

        protected void FixedUpdate()
        {
            RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            Hit();
            //Destroy(gameObject);
        }

        protected virtual void Hit()
        {
        }

        protected virtual void BulletInit()
        {
            // 초기화
            RbCompo = GetComponent<Rigidbody>();
        }
    }
}