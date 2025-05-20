using System;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combats;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public abstract class BaseBullet : MonoBehaviour, IPoolable
    {
        [field: SerializeField] protected BulletSettingSO BulletSO; // 총알 데이터 받기

        [SerializeField] private string itemName;
        [HideInInspector] public PlayerAttackCompo _attackCompo;

        protected bool isRotModle;

        public string PoolingName => itemName;
        
        protected Vector3 fireDirection;

        public bool IsPlayerFollow;
        
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
            if (IsPlayerFollow)
            {
                if (isRotModle)
                {
                    if (isSlowy)
                        RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed / SlowyDegree;
                }
                else
                {
                    if (isSlowy)
                        RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed / SlowyDegree;

                }

                RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed;
            }
            else
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
                    if (isSlowy)
                        RbCompo.linearVelocity = transform.forward * BulletSO.BulletSpeed / SlowyDegree;
                    else
                        RbCompo.linearVelocity = transform.forward * BulletSO.BulletSpeed;

                }

            }

        }
        protected virtual void DestroyBullet(IPoolable pool)
        {
            PoolManager.Instance.Push(pool);
        }


        protected virtual void Hit(Collider hitable)
        {
            Debug.Log(hitable.gameObject);
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