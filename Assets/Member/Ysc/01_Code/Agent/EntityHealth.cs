using DG.Tweening;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine;

namespace Member.Ysc._01_Code.Agent
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSO hpStat;
        public float maxHealth;
        [SerializeField] private float _currentHealth;

        private Entity _entity;
        private EntityStat _statCompo;
        private EntityFeedbackData _feedbackData;

        [HideInInspector] public NotifyValue<float> Hp = new();

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo ??= _entity.GetCompo<EntityStat>();
            _feedbackData ??= _entity.GetCompo<EntityFeedbackData>();
        }

        public void AfterInit()
        {
            _statCompo.GetStat(hpStat).OnValueChange += HandleHPChange;
            _currentHealth = maxHealth = _statCompo.GetStat(hpStat).Value;
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
        
        public void ApplyDamage(float damage, Vector2 direction)
        {
            if (_entity.IsDead || _entity.IsInvin) return;

            Debug.Log(direction);
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
            _feedbackData.LastAttackDirection = direction.normalized;
            Hp.Value = _currentHealth;
            AfterHitFeedbacks();
        }

        public float GetCurrentHp()
        {
            return _currentHealth;
        }

        public void ApplyHeal(float heal,float duration)
        {
            if (_entity.IsDead) return;

            float startValue = _currentHealth;
            float endValue = Mathf.Clamp(_currentHealth + heal, 0, maxHealth);

            DOTween.To(
                () => startValue,
                value =>
                {
                    _currentHealth = Mathf.Clamp(value, 0, maxHealth);
                    Hp.Value = _currentHealth;
                },
                endValue,
                duration
            );
        }

        private void AfterHitFeedbacks()
        {
            _entity.OnHit?.Invoke();

            if (_currentHealth <= 0)
            {
                _entity.OnDead?.Invoke();
            }
                
        }
    }
}
