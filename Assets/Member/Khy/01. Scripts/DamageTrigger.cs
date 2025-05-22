using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combats;
using System;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public float timer;
    private float _timer;
    private bool isTimerStart;

    [SerializeField]
    private LayerMask _whatIsEnemy;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.gameObject.layer == _whatIsEnemy)
        Hit(other);
    }

    private void Hit(Collider hitable)
    {
        if (hitable.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(5f);
    }
}
