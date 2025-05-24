using JetBrains.Annotations;
using Member.Ysc._01_Code.Containers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Member.Ysc._01_Code.Combat.Bullet
{
    public class LaserBullet : BaseBullet
    {
        [SerializeField] private LayerMask whatIsPlayer;
        
        [SerializeField] private Transform fireEndTrm;
        
        [SerializeField] private LineRenderer lineRenderer;

        private float _originWidth;
        private Transform _originFireEndTrm;
        protected override void BulletInit()
        {
            base.BulletInit();
            _originWidth = lineRenderer.widthMultiplier;

        }

        protected override void FixedUpdate()
        {
            if (lineRenderer.widthMultiplier > 0)
            {
                lineRenderer.widthMultiplier = Mathf.Lerp(lineRenderer.widthMultiplier, 0, Time.fixedDeltaTime * 10);
            }
            else
            {
                fireEndTrm = _originFireEndTrm;
                DestroyBullet(this);
            }
        }

        public override void SetTransform(TargetContainer? targetTrm = null)
        {
            base.SetTransform(targetTrm);

            Debug.Log($"{targetTrm}");
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, Quaternion.Euler(0, 180, 0) *fireEndTrm.position);
            if (fireContainer != null)
            {
                Vector3 pos = fireContainer.Value.targetPos;
                lineRenderer.SetPosition(1, pos);
            }

            CheckPlayer();

        }

        private void CheckPlayer()
        {
            Vector3 p1 = lineRenderer.GetPosition(0);
            Vector3 p2 = lineRenderer.GetPosition(1);
            Ray ray = new Ray(p1, (p2 - p1).normalized);
            if (Physics.SphereCast(ray, 3f, out RaycastHit hitInfo, 200, whatIsPlayer))
            {
                Hit(hitInfo.collider);
            }
            
        }

        public override void ResetItem()
        {
            base.ResetItem();

            _originFireEndTrm = fireEndTrm;
            lineRenderer.widthMultiplier = _originWidth; 
            lineRenderer.useWorldSpace = true;
        }
    }
}
