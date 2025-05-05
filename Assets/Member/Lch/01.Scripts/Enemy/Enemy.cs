using System;
using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using NUnit.Framework;
using Unity.Behavior;
using UnityEngine;

public abstract class Enemy :Entity
{
    [SerializeField] private Transform deadPoint;
    
    protected BehaviorGraphAgent btAgent;
    protected StateEventChange _stateChannel;
    [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }

    public bool IsDeadEnd { get; protected set; } = false;
    
    private Sequence _sequence;
    
    
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        btAgent = GetComponent<BehaviorGraphAgent>();
        Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();

        _sequence?.Kill();
        _sequence = null;
    }

    public void LookTarget(Transform target)
    {
        transform.LookAt(target);
    }

    public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
    {
        if (btAgent.GetVariable(key, out BlackboardVariable<T> result))
        {
            return result;
        }

        return default;
    }

    protected virtual void Start()
    {

    }

    public void EnemyDead()
    {
        // movement.isMove = false;
        if (_sequence.IsActive())
            _sequence.Kill();
        
        Debug.Assert(deadPoint != null, $"This GameObject deadPoint is null : {gameObject.name}");
        
        Debug.Log(IsDead);
        
        Tween tween = transform.DORotate(new Vector3(-35f, 0f, 0f), 0.3f, RotateMode.Fast);
        Tween tween2 = transform.DOMove(deadPoint.position, 7f).OnComplete(() => IsDeadEnd = true);

        _sequence = DOTween.Sequence()
            .Append(tween)
            .Append(tween2);

        _sequence.Play();
    }



}
