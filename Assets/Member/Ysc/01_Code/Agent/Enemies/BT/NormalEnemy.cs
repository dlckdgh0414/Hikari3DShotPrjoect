using System;
using DG.Tweening;
using UnityEngine;

namespace Member.Ysc._01_Code.Agent.Enemies.BT
{
    public class NormalEnemy : BTEnemy
    {
        [SerializeField] private bool isMove;
        private EnemyMovement movement;

        protected override void Awake()
        {
            base.Awake();
            movement = GetCompo<EnemyMovement>();
        }

        private void Update()
        {
            movement.isMove = isMove;
        }

        [ContextMenu("Enemy Dead")]
        public void EnemyDead()
        {
            
            movement.isMove = false;
            transform.DORotate(new Vector3(-35f, 0f, 0f), 0.5f, RotateMode.Fast)
                .OnUpdate(() =>
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.forward.y, transform.forward.z * -0.4f);
                    transform.DOMove(pos, 10f);
                });
        } 
    }
}