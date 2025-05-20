using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.Ysc._01_Code.Combat.Attacker
{
    public class LaserAttack : Attack
    {
        [SerializeField] private GameObject shotFrame;
        
        [SerializeField] private float coolTime = 1.5f;

        
        private float _currentTime = 0;

        private void Start()
        {
            shotFrame.SetActive(false);
        }

        public override void EnemyAttack(Transform target, float timer)
        {
            if (!Mathf.Approximately(_currentTime, 0)) return;
            shotFrame.SetActive(true);
            bool isGuided = Random.value <= 0.7f;

            if (true)
            {
                Quaternion lookRotation = Quaternion.LookRotation(target.position);
                shotFrame.transform.rotation = lookRotation;
            }
            
            while (true)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= coolTime)
                {
                    _currentTime = 0;
                    shotFrame.SetActive(false);
                    break;
                }
            }
            SpawnBullet(target, timer, isGuided);
        }
    }
}