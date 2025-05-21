using Member.Ysc._01_Code.Combat.Bullet;
using System;
using UnityEngine;

public class ChargingPassiveSkill : PassiveSkill
{
    private float _pressTime;
    [Header("Â÷Â¡ ½Ã°£")]
    public float maxChargingTime;
    private bool isPress;
    private readonly string chargeVfx = "ChargingVFX";
    public event Action OnChargeShoot;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        _player.InputReader.OnChargingEvent += ChargingHandle;
    }

    private void ChargingHandle(bool isclick)
     =>   isPress = isclick;
        
    public override void PassiveAbility()
    {
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
            }
            _pressTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        OnChargeShoot?.Invoke();
    }
}
