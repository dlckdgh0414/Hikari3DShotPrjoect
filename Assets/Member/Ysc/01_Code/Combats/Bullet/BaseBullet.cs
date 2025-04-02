using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [field: SerializeField] public BulletSettingSO BulletSO { get; protected set; } // 총알 데이터 받기
        
        protected void Awake()
        {
            BulletInit();
        }

        protected virtual void BulletInit() { } // 초기화
    }
}