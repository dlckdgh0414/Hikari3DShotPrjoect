using Member.Ysc._01_Code.Combat.Attacker;
using UnityEngine;

namespace Member.Ysc._01_Code.Agent.Enemies.BT
{
    public class LaserEnemy : BTEnemy
    {
        protected override void HandleDead()
        {
            base.HandleDead();

            LaserAttack attack = GetComponentInChildren<LaserAttack>();
            if (attack == null)
            {
                Debug.LogError($"Attack Type of null {attack.name}");
                return;
            }

            attack.FrameControl(false);
        }
    }
}