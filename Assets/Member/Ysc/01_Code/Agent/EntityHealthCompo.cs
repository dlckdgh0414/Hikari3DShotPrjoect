using DG.Tweening;
using Member.Ysc._01_Code.StatSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Member.Ysc._01_Code.Agent
{
    public class EntityHealthCompo : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSO hpStat;
        public float maxHealth;
        [SerializeField] public float CurrentHealth { get; private set; }

        private Entity _entity;
        private EntityStat _statCompo;
        private EntityFeedbackData _feedbackData;

        [HideInInspector] public NotifyValue<float> Hp = new();

        [SerializeField]
        private Slider hpSlider;

        public bool IsRevived { get; set; }

        private SkillCompo _skillCompo;

        [ContextMenu("Test")]
        public void Test()
        {
            ApplyDamage(20);
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo ??= _entity.GetCompo<EntityStat>();
            _feedbackData ??= _entity.GetCompo<EntityFeedbackData>();
            _skillCompo = entity.GetCompo<SkillCompo>();
        }

        public void AfterInit()
        {
            _statCompo.GetStat(hpStat).OnValueChange += HandleHPChange;
            CurrentHealth = maxHealth = _statCompo.GetStat(hpStat).Value;
            _entity.OnDamage += ApplyDamage;
        }


        private void OnDestroy()
        {
            _statCompo.GetStat(hpStat).OnValueChange -= HandleHPChange;
        }

        private void HandleHPChange(StatSO stat, float current, float previous)
        {
            maxHealth = current;
            CurrentHealth = Mathf.Clamp(CurrentHealth + current - previous, 1f, maxHealth);
        }
        
        public void ApplyDamage(float damage)
        {
            if (_entity.IsDead || _entity.IsInvin) return;

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
            Hp.Value = CurrentHealth;
            AfterHitFeedbacks();
        }

        public float GetCurrentHp()
        {
            return CurrentHealth;
        }

        public void ApplyHeal(float heal,float duration)
        {
            if (_entity.IsDead) return;

            float startValue = CurrentHealth;
            float endValue = Mathf.Clamp(CurrentHealth + heal, 0, maxHealth);

            DOTween.To(
                () => startValue,
                value =>
                {
                    CurrentHealth = Mathf.Clamp(value, 0, maxHealth);
                    Hp.Value = CurrentHealth;
                },
                endValue,
                duration
            );
        }

        private void AfterHitFeedbacks()
        {
            _entity.OnHit?.Invoke();

            if (CurrentHealth <= 0)
            {
                if(IsRevived)
                {
                    _skillCompo.GetSkill<RevivedPassive>().Revived();
                }
                else
                {
                    Entity.IsGameStart = false;
                    _entity.OnDead?.Invoke();
                }
            }
                
        }

    }
}
