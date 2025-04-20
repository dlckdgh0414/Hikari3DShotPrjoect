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
    }
}