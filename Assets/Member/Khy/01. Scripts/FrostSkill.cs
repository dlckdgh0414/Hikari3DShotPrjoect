using Ami.BroAudio;
using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using UnityEngine;

public class FrostSkill : ActiveSkill
{
    [Header("¸ØÃß´Â ½Ã°£")]
    public float duration=3f;
    [Header("¸ØÃß´Â ½Ã°£")]
    public float damage = 3f;
    private readonly string frostEffect = "FrostVFX";
    private readonly string enemyFrostEffect = "Iceing";
    [SerializeField] private SoundID iceSound;

    public override void UseSkill()
    {
        base.UseSkill();
        entityVFX.PlayVfx(frostEffect, Vector3.zero, Quaternion.identity);
        BroAudio.Play(iceSound);
        foreach (Enemy obj in EnemyManager.Enemies)
        {
            Debug.Log(obj);
            obj.GetCompo<EnemyMovement>().isMove = false;
            obj.GetCompo<EntityVFX>().PlayVfx(enemyFrostEffect, Vector3.zero, Quaternion.identity);
            obj.GetCompo<EntityHealthCompo>().ApplyDamage(damage);
        }
        DOVirtual.DelayedCall(duration, () => {
            foreach (Enemy obj in EnemyManager.Enemies)
            {
                Debug.Log(obj);
                obj.GetCompo<EnemyMovement>().isMove = true;
            }
        });
    }

    
}
