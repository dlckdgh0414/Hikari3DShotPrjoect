using UnityEngine;

namespace Member.Ysc._01_Code.Agent.Enemy
{
    public class Enemy : Entity
    {
        protected override void HandleHit()
        {
            Debug.Log("핸들 힛");
        }

        protected override void HandleDead()
        {
            Debug.Log("핸들 데드");
        }

        protected override void AfterInitialize()
        {
            Debug.Log("에너미 후 초기화");
            base.AfterInitialize();
        }
    }
}
