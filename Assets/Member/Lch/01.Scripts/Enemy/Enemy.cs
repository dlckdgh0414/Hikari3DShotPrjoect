using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using Unity.Behavior;
using UnityEngine;

public abstract class Enemy :Entity
{
    protected BehaviorGraphAgent btAgent;
    protected StateEventChange _stateChannel;
    [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }

    public bool IsDeadEnd {get; protected set; }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        btAgent = GetComponent<BehaviorGraphAgent>();
        Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
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

   
}
