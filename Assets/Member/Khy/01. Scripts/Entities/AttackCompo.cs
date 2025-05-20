using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine;

public class AttackCompo : MonoBehaviour
{
    public StatSO atkStat;
    public float BulletDamage
    {
        get => atkStat.Value;
    }

    protected EntityStat _statCompo;
}
