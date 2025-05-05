using Member.Ysc._01_Code.StatSystems;
using UnityEngine;

namespace Member.Ysc._01_Code.Agent
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSO hpStat;
        public float maxHealth = 0f;
        [SerializeField]private float _currentHealth;

        private Entity _entity;
        private EntityStat _statCompo;
        private EntityFeedbackData _feedbackData;
        [SerializeField] private HealthGageAdjuster hpBar;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void AfterInit()
        {
            _statCompo = _entity.GetCompo<EntityStat>();
            _feedbackData = _entity.GetCompo<EntityFeedbackData>();
            _statCompo.GetStat(hpStat).OnValueChange += HandleHPChange;
            maxHealth = _statCompo.GetStat(hpStat).Value;
            _currentHealth = maxHealth;
            _entity.OnDamage += ApplyDamage;
        }


        private void OnDestroy()
        {
            _statCompo.GetStat(hpStat).OnValueChange -= HandleHPChange;
        }

        private void HandleHPChange(StatSO stat, float current, float previous)
        {
            maxHealth = current;
            _currentHealth = Mathf.Clamp(_currentHealth + current - previous, 1f, maxHealth);
        }

        public void ApplyDamage(float damage, Vector2 direction, Entity dealer)
        {
            if (_entity.IsDead) return;
            
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
            _feedbackData.LastAttackDirection = direction.normalized;
            _feedbackData.LastEntityWhoHit = dealer;

            AfterHitFeedbacks();
        }

        private void AfterHitFeedbacks()
        {
            _entity.OnHit?.Invoke();

            if(hpBar != null)
            {
                hpBar.ApplyHealth(_currentHealth);
            }

            if (_currentHealth <= 0)
            {
                _entity.OnDead?.Invoke();
            }
                
        }
    }
}
