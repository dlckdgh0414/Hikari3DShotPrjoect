using System;
using JetBrains.Annotations;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combats;
using Member.Ysc._01_Code.Containers;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public abstract class BaseBullet : MonoBehaviour, IPoolable
    {
        [field: SerializeField] protected BulletSettingSO BulletSO; // 총알 데이터 받기
        [SerializeField] private string itemName;

        protected bool isRotModle;

        public string PoolingName => itemName;
        
        protected Vector3 fireDirection;
        protected TargetContainer? fireContainer;

        public bool IsPlayerFollow;
        
        public Rigidbody RbCompo { get; protected set; }
        public int GetBulletCount => BulletSO.BulletCount;

        public static bool isSlowy;
        public static float SlowyDegree;

        public void SetDirection(Vector3 targetPos)
        { 
            fireDirection = targetPos - transform.position;
        }

        public virtual void SetTransform([CanBeNull] TargetContainer? targetTrm = null)
        {
            if (targetTrm != null)
                fireContainer = targetTrm;
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
                    else
                        RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed;
                }
                else
                {
                    if (isSlowy)
                        RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed / SlowyDegree;
                    else
                        RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed;
                }
                LoockTarget();
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
            Debug.Log("히히 들어간당");
            PoolManager.Instance.Push(pool);
        }


        protected virtual void Hit(Collider hitable)
        {
            if (hitable.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(BulletSO.BulletDamage);
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
        public virtual void ResetItem()
        {
        }

        private void OnValidate()
        {
            itemName = gameObject.name;
        }

        
    }
}