using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Member.Ysc._01_Code.Containers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.Ysc._01_Code.Combat.Attacker
{
    public class LaserAttack : Attack,IEntityComponent
    {
        [SerializeField] private List<LineRenderer> shotFrameList;
        
        [SerializeField] private float coolTime;
        
        private bool _isCooltime = false;
        private List<Vector3> _originPoints;
        private readonly string warningVFXName = "Warning";
        private EntityVFX _entityVFX;

        public void Initialize(Entity entity)
        {
            _entityVFX = entity.GetCompo<EntityVFX>();
        }

        private void OnEnable()
        {
            foreach (var shotFrame in shotFrameList)
            {
                shotFrame.SetPosition(0, transform.position);
                LineControl();
            }
        }

        public void InitLaser()
        {

            foreach (var shotFrame in shotFrameList)
            {
                shotFrame.SetPosition(0, transform.position);
                shotFrame.SetPosition(1, new Vector3(shotFrame.GetPosition(0).x, shotFrame.GetPosition(0).y, shotFrame.GetPosition(1).z));
                LineControl();
            }
            
            _originPoints = shotFrameList.Select(x => x.GetPosition(1)).ToList();
        }


        public override void EnemyAttack(Transform target, float timer)
        {
            if (_isCooltime) return;
            bool isGuided = Random.value <= 0.7f;
            Transform targetTrm = target;
            if (isGuided)
            {
                Debug.Log($"<color=red>타겟 : {target}</color>");
                foreach (var shotFrame in shotFrameList)
                {
                    shotFrame.SetPosition(1, targetTrm.position);
                }
            }
            LineControl(true);
            _entityVFX.PlayVfx(warningVFXName, new Vector3(0, 0, 0), Quaternion.identity);
            StartCoroutine(ShotDelayCoroutine(coolTime, targetTrm, timer, isGuided));
        }

        public void LineControl(bool isActive = false)
        {
            foreach (var shotFrame in shotFrameList)
            {
                shotFrame.enabled = isActive;
            }
        }
        
        private IEnumerator ShotDelayCoroutine(float time, Transform target, float timer, bool isGuided = false)
        {
            TargetContainer targetContainer = new TargetContainer
            {
                targetTrm = target,
                targetPos = target.position
            };

            _isCooltime = true;
            while (true)
            {
                Debug.Log($"<color=red>{targetContainer.targetPos}</color>");
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    yield return null;
                }
                else
                {
                    _entityVFX.StopVfx(warningVFXName);
                    LineControl(false);
                    for (int i = 0; i < shotFrameList.Count; i++)
                    {
                        shotFrameList[i].SetPosition(1, _originPoints[i]);
                    }
                    SpawnBullet(targetContainer, timer, isGuided);
                    _isCooltime = false;
                    yield break;
                }
            }
        }

        
    }
}
