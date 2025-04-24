using System;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combats;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public abstract class BaseBullet : MonoBehaviour, IPoolable
    {
        [field: SerializeField] private BulletSettingSO BulletSO; // 총알 데이터 받기

        [SerializeField] private string itemName;
        
        public string PoolingName => itemName;
        
        protected Vector3 fireDirection;
        
        public Rigidbody RbCompo { get; protected set; }
        public int GetBulletCount => BulletSO.BulletCount;

        public void SetDirection(Vector3 direction)
        {
            fireDirection = direction - transform.position ;
        }
        
        protected virtual void Awake()
        {
            BulletInit();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) return;
            Hit(other);
            DestroyBullet(this);
        }

        protected void FixedUpdate()
        {
            RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed;
        }

        protected virtual void DestroyBullet(IPoolable pool)
        {
            PoolManager.Instance.Push(pool);
        }


        protected virtual void Hit(Collider hitable)
        {
            if (hitable.TryGetComponent(out IDamageable damageable))
            {
                Vector2 direction = (hitable.transform.position - transform.position).normalized;
                damageable.ApplyDamage(BulletSO.bulletDamage, direction);
            }
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

        private void OnValidate()
        {
            gameObject.name = PoolingName;
        }
    }
}