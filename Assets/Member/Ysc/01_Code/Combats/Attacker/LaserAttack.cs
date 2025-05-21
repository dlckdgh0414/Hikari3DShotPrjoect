using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.Ysc._01_Code.Combat.Attacker
{
    public class LaserAttack : Attack
    {
        [SerializeField] private List<GameObject> shotFrameList;
        
        [SerializeField] private float coolTime;
        
        private bool _isCooltime = false;
        private List<Quaternion> _originRotations;

        private void OnEnable()
        {
            shotFrameList.ForEach(obj => FrameControl(false));
            _originRotations = shotFrameList.Select(obj => obj.transform.rotation).ToList();
        }

        public override void EnemyAttack(Transform target, float timer)
        {
            if (_isCooltime) return;
            FrameControl(true);
            bool isGuided = Random.value <= 0.7f;
            if (true)
            {
                Debug.Log($"<color=red>타겟 : {target}</color>");
                foreach (var shotFrame in shotFrameList)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(target.position - shotFrame.transform.position);
                    shotFrame.transform.rotation = lookRotation;
                }
            }
            StartCoroutine(ShotDelayCoroutine(coolTime, target, timer));
        }

        private void FrameControl(bool isActive = false)
        {
            foreach (var shotFrame in shotFrameList)
            {
                shotFrame.SetActive(isActive);
            }
        }
        
        private IEnumerator ShotDelayCoroutine(float time, Transform target, float timer, bool isGuided = false)
        {
            _isCooltime = true;
            while (true)
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    yield return null;
                }
                else
                {
                    FrameControl(false);
                    for (int i = 0; i < shotFrameList.Count; i++)
                    {
                        shotFrameList[i].transform.rotation = _originRotations[i];
                    }
                    SpawnBullet(target, timer, isGuided);
                    _isCooltime = false;
                    yield break;
                }
            }
        }
    }
}
