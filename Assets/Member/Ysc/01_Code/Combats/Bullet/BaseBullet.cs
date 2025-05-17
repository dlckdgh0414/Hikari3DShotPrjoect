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
        [HideInInspector] public PlayerAttackCompo _attackCompo;

        protected bool isRotModle;

        public string PoolingName => itemName;
        
        protected Vector3 fireDirection;
        
        public Rigidbody RbCompo { get; protected set; }
        public int GetBulletCount => BulletSO.BulletCount;

        public static bool isSlowy;
        public static float SlowyDegree;

        public void SetDirection(Vector3 direction)
        {
            fireDirection = direction - transform.position;
        }

        protected virtual void Awake()
        {
            BulletInit();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
          
        }

        protected virtual void FixedUpdate()
        {
            if (isRotModle)
            {
                if (isSlowy)
                    RbCompo.linearVelocity = -Vector3.forward * BulletSO.BulletSpeed / SlowyDegree;
                else
                    RbCompo.linearVelocity = -Vector3.forward * BulletSO.BulletSpeed;
            }
            else
            {
                if(isSlowy) 
                    RbCompo.linearVelocity = transform.forward * BulletSO.BulletSpeed/SlowyDegree;
                else
                    RbCompo.linearVelocity = transform.forward * BulletSO.BulletSpeed;

            }

        }

        protected void LoockTarget()
        {
            if (isRotModle)
            {
                Quaternion quaternion = Quaternion.LookRotation(fireDirection);
                float anglez = quaternion.eulerAngles.z;
                transform.rotation = Quaternion.Euler(90, quaternion.eulerAngles.y, anglez);
            }
            else
            {
                Quaternion quaternion = Quaternion.LookRotation(fireDirection);
                transform.rotation = quaternion;
            }
        }

        protected virtual void DestroyBullet(IPoolable pool)
        {
            PoolManager.Instance.Push(pool);
        }


        protected virtual void Hit(Collider hitable)
        {
            if (hitable.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_attackCompo.BulletDamage);
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
            itemName = gameObject.name;
        }

        
    }
}