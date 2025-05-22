using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour, IEntityComponent, IAfterInit
{
    protected EntityHealthCompo _entityHealth;
    private float _maxHp;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _backSlider;
    [SerializeField] private TextMeshProUGUI text;

    public void AfterInit()
    {
        _maxHp = _entityHealth.maxHealth;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _maxHp;
        if (_backSlider != null)
        {
            _backSlider.maxValue = _maxHp;
            _backSlider.value = _maxHp;
        }
    }

    public void Initialize(Entity entity)
    {
        _entityHealth = entity.GetCompo<EntityHealthCompo>();
        _entityHealth.Hp.OnValueChanged += ChangeHp;
    }

    private void ChangeHp(float prev, float next)
    {
        _hpSlider.value = _entityHealth.CurrentHealth;
        text.text = $"{Math.Truncate(_entityHealth.CurrentHealth)}";

        if (_backSlider != null && _backSlider.value > _hpSlider.value)
        {
            DOTween.Sequence()
                .AppendInterval(0.2f)
                .Append(_backSlider.DOValue(_entityHealth.CurrentHealth, 0.5f).SetEase(Ease.OutCubic));
        }
        else if (_backSlider != null && _backSlider.value < _hpSlider.value)
        {
            _backSlider.value = _entityHealth.CurrentHealth;
        }
    }
}
