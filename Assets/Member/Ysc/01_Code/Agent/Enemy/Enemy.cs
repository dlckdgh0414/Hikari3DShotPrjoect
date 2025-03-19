using UnityEngine;

namespace Member.Ysc._01_Code.Agent.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        protected void Awake()
        {
            Initialize();
            AfterInit();
        }

        private void Initialize()
        {
            Debug.Log("에너미 초기화");
        }
        
        protected virtual void AfterInit()
        {
            Debug.Log("에너미 후 초기화");
        }
    }
}
