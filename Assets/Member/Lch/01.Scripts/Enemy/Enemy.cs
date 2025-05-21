using System;
using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using NUnit.Framework;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy :Entity, IPoolable
{
    [SerializeField] private Transform deadPoint;
    
    protected BehaviorGraphAgent btAgent;
    protected StateEventChange _stateChannel;
    [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }

    public UnityEvent OnRealDead;

    public bool IsDeadEnd { get; protected set; } = false;
    
    private Sequence _sequence;
    public string PoolingName => gameObject.name;
    
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        btAgent = GetComponent<BehaviorGraphAgent>();
        Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
    }

    private void OnEnable()
    {
        EnemyManager.Register(this);
    }

    private void OnDisable()
    {
        EnemyManager.Unregister(this);
        _sequence?.Kill();
        _sequence = null;
        OnRealDead?.Invoke();
    }
    
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    {
        IsDeadEnd = false;
        IsDead = false;
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

    public void DestroyEnemy()
    {
        PoolManager.Instance.Push(this);
    }

    public void EnemyDead()
    {
        // movement.isMove = false;
        if (_sequence.IsActive())
            _sequence.Kill();
        
        Debug.Assert(deadPoint != null, $"This GameObject deadPoint is null : {gameObject.name}");
        
        Debug.Log(IsDead);
        
        Tween tween = transform.DORotate(new Vector3(-35f, 0f, 0f), 0.2f, RotateMode.Fast);
        Tween tween2 = transform.DOMove(deadPoint.position, 3f).OnComplete(() => IsDeadEnd = true);

        _sequence = DOTween.Sequence()
            .Append(tween)
            .Append(tween2);

        _sequence.Play();
    }
}
