using Member.Ysc._01_Code.Combat.Bullet;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChargingPassiveSkill : PassiveSkill
{
    private float _pressTime;
    [Header("Â÷Â¡ ½Ã°£")]
    public float maxChargingTime;
    private bool isPress;
    private readonly string chargeVfx = "ChargingVFX";
    public event Action OnChargeShoot;
    [Header("Â÷Â¡ ½½¶óÀÌ´õ")]
    public Slider chargeSlider;

    private PlayerAttackCompo _attackCompo;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        _player.InputReader.OnChargingEvent += ChargingHandle;
        chargeSlider.maxValue = maxChargingTime;
    }
    private void OnDestroy()
    {
        if (skillEnabled)
            _player.InputReader.OnChargingEvent -= ChargingHandle;
    }
    private void ChargingHandle(bool isclick)
     => isPress = isclick;
        
    public override void PassiveAbility()
    {
        chargeSlider.value = _pressTime;
        base.PassiveAbility();
        if (isPress)
        {
            if (!(_pressTime > maxChargingTime))
            {
                entityVFX.PlayVfx(chargeVfx,Vector3.zero,Quaternion.identity);
                _pressTime += Time.deltaTime;
            }
        }
        else if (!isPress && _pressTime >= 0)
        {
            if (_pressTime > maxChargingTime)
            {
                Shoot();
                _pressTime = 0f;
            }
            _pressTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        _attackCompo.ChargeShoot();
    }
}
