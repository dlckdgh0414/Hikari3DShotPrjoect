using UnityEngine;
using DG.Tweening;
using Member.Ysc._01_Code.Combats;

public class DamagePassiveBullet : MonoBehaviour,IPoolable
{
    [SerializeField]
    private BulletSettingSO bullet;
    [HideInInspector]
    public Transform target;
    public float flightDuration = 1.5f;
    public float curveVariance = 5f;

    [SerializeField] private string itemName;

    private Vector3 startPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;

    public string PoolingName => itemName;

    void Start()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        startPoint = transform.position;
        endPoint = target.position;

        // 중간 지점 + 랜덤 방향으로 휘는 곡선
        Vector3 mid = (startPoint + endPoint) / 2f;

        // 🎯 모든 방향으로 랜덤하게 휘게 하기!
        Vector3 offset = Random.onUnitSphere * curveVariance;
        controlPoint = mid + offset;

        // 경로 만들기 (중간 제어점 + 끝점)
        Vector3[] path = new Vector3[] { controlPoint, endPoint };

        transform.DOPath(path, flightDuration, PathType.CatmullRom)
                 .SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(bullet.BulletDamage);
            DestroyBullet(this);
        }
    }
    protected virtual void DestroyBullet(IPoolable pool)
    {
        PoolManager.Instance.Push(pool);
    }
//    void OnDrawGizmos()
//    {
//#if UNITY_EDITOR
//        if (target == null) return;

//        Gizmos.color = Color.yellow;

//        Vector3 p0 = startPoint;
//        Vector3 p1 = controlPoint;
//        Vector3 p2 = endPoint;

//        Vector3 prevPoint = p0;
//        int segments = 20;

//        for (int i = 1; i <= segments; i++)
//        {
//            float t = i / (float)segments;
//            Vector3 m1 = Vector3.Lerp(p0, p1, t);
//            Vector3 m2 = Vector3.Lerp(p1, p2, t);
//            Vector3 point = Vector3.Lerp(m1, m2, t);
//            Gizmos.DrawLine(prevPoint, point);
//            prevPoint = point;
//        }
//#endif
//    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    {
    }
}
