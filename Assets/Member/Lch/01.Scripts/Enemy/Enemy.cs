using System;
using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public abstract class Enemy :Entity, IPoolable
{
    [SerializeField] private int minCurrency = 5;
    [SerializeField] private int maxCurrency = 10;
    
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

    protected virtual void OnEnable()
    {
        EnemyManager.Register(this);
    }

    protected virtual void OnDisable()
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
        transform.rotation = Quaternion.identity;
        gameObject.layer = LayerMask.NameToLayer("Enemy");
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

    public abstract void InitObject();

    public void EnemyDead()
    {
        // movement.isMove = false;
        if (_sequence.IsActive())
            _sequence.Kill();
        
        Debug.Assert(deadPoint != null, $"This GameObject deadPoint is null : {gameObject.name}");
        
        Tween tween = transform.DORotate(new Vector3(-35f, 0f, 0f), 0.2f, RotateMode.Fast);
        Tween tween2 = transform.DOMove(deadPoint.position, 1.4f).OnComplete(() => IsDeadEnd = true);

        _sequence = DOTween.Sequence()
            .Append(tween)
            .Append(tween2);

        _sequence.Play();
        CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, Random.Range(minCurrency, maxCurrency+1));
    }
}
