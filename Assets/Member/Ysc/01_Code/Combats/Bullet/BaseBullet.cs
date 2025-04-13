using System;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combats;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public abstract class BaseBullet : MonoBehaviour,IPoolable
    {
        [field: SerializeField] private BulletSettingSO BulletSO; // 총알 데이터 받기
        
        protected Vector3 fireDirection;
        
        public Rigidbody RbCompo { get; protected set; }
        public int GetBulletCount => BulletSO.BulletCount;

        public string ItemName => "Bullet";

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
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            PoolManager.Instance.Push(this);
        }


        protected virtual void Hit()
        {
        }

        protected virtual void BulletInit()
        {
            // 초기화
            RbCompo = GetComponent<Rigidbody>();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void ResetItem()
        {
        }
    }
}