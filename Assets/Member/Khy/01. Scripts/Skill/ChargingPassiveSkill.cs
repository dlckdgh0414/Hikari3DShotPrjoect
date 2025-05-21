using UnityEngine;

public class ChargingPassiveSkill : PassiveSkill
{
    private float _pressTime;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        _player.InputReader.OnChargingEvent += ChargingHandle;
    }

    private void ChargingHandle(bool isPress)
    {
        if(isPress)
        {
            _pressTime += Time.deltaTime;
        }
        else if(!isPress && _pressTime >= 0)
        {
            _pressTime -= Time.deltaTime;
        }
    }

    public override void PassiveAbility()
    {
        base.PassiveAbility();
        //_player.InputReader.
        //Debug.Log("ÆÐ½Ãºê");
    }
}
